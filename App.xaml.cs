using LungDatabaseAccess.Services;
using LungDatabaseAccess.Services.IServices;
using LungoApp.Windows;
using LungoDatabase.DataAccess;
using LungoDatabaseAccess.Services;
using LungoModels;
using LungoViewModels;
using LungoViewModels.Stores;
using LungoViewModels.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LungoApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddSingleton(getDatabaseOptions());
                services.AddSingleton(new WordProgressEventAggregator());
                services.AddSingleton(new NewWordContextEventAggregator());
                services.AddSingleton(this);
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<IAppServices, AppServices>();
                services.AddSingleton((s) => new MainViewModel(s.GetRequiredService<NavigationStore>(),
                    new LungoViewModels.IHostModel() { _appHost = _host}));
                services.AddSingleton<MainWindow>();

            }).Build();
            _navigationStore = new NavigationStore();
            App.Current.Properties["AppHost"] = _host;
        }

        protected async override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            AppServices appServices = (AppServices)_host.Services.GetRequiredService<IAppServices>();
            bool isAppRunningFirstTime = appServices.isAppRunningFirstTime();
            if (isAppRunningFirstTime)
            {
                firstTimeRunningSequence();
            }
            else
            {
                launchMainWindow();
                DbContextOptions options = _host.Services.GetRequiredService<DbContextOptions>();
                using (LungoContextDB context = new LungoContextDB(options))
                {
                    SettingServices settingServices = new SettingServices(context);
                    bool IsUserRegistered = await settingServices.checkIfUserRegistered();
                    if (!IsUserRegistered) {
                        Guid guid;
                        string userPublicId = await settingServices.registerUser();
                        if(Guid.TryParse(userPublicId, out guid))
                        {
                            await settingServices.setPublicIdOfUser(userPublicId);
                        }
                    }
                    SettingServices.getAllPublicUsersOfApplication();
                }

            }
            base.OnStartup(e);
        }

        public void launchMainWindow()
        {
            
            SetLanguageDictionary();
            //MainWindow window = new MainWindow() { DataContext = new MainViewModel(_navigationStore) };
            MainWindow window = _host.Services.GetRequiredService<MainWindow>();
            window.DataContext = _host.Services.GetRequiredService<MainViewModel>();
            window.Show();


        }

        private DbContextOptions getDatabaseOptions()
        {
            var optionBuilder = new DbContextOptionsBuilder<LungoContextDB>();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            optionBuilder.UseSqlServer(connectionString);

            return optionBuilder.Options;
        }

        private void firstTimeRunningSequence()
        {
            AppServices appServices = (AppServices)_host.Services.GetRequiredService<IAppServices>();
            //appServices.addAllLanguagesToDB();
            FirstTimeSignUpWindow firstTimeSignUp = new FirstTimeSignUpWindow(this);
            firstTimeSignUp.Show();
        }

        private void SetLanguageDictionary()
        {
            ResourceDictionary dict = new ResourceDictionary();
            switch (Thread.CurrentThread.CurrentCulture.ToString())
            {
                case "en-US":
                    dict.Source = new Uri("..\\ResourceDictionary\\LangEnglish.xaml", UriKind.Relative);
                    break;
            }

            this.Resources.MergedDictionaries.Add(dict);
        }
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            _host.Dispose();
            IHost mainHost = (IHost)App.Current.Properties["MainViewModelHost"];
            if (mainHost != null)
            {
                mainHost.Dispose();
            }

        }
    }
}
