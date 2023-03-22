using LungDatabaseAccess.Services;
using LungDatabaseAccess.Services.IServices;
using LungoApp.Windows;
using LungoDatabase.DataAccess;
using LungoDatabaseAccess.Services;
using LungoDatabaseAccess.Services.Implementations;
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
using System.Reactive.Disposables;
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
        private IHost _host;
        private NavigationStore _navigationStore;

        public App()
        {
            InitializeHost();
            InitializeNavigationStore();
            
        }
        private void InitializeNavigationStore()
        {
            _navigationStore = new NavigationStore();
        }
        private void InitializeHost()
        {
            _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddDbContext<LungoContextDB>(options => getDatabaseOptions(options), ServiceLifetime.Transient);
                services.AddSingleton(new WordProgressEventAggregator());
                services.AddSingleton(new NewWordContextEventAggregator());
                services.AddSingleton(this);
                services.AddSingleton<NavigationStore>();
                services.AddSingleton((s) => new MainViewModel(s.GetRequiredService<NavigationStore>(),
                    new LungoViewModels.IHostModel() { _appHost = _host }));
                services.AddSingleton<MainWindow>();

                services.AddTransient<IAppServices, AppServices>();
                services.AddTransient<IUserServices, UserServices>();
                services.AddTransient<ICollectionServices, CollectionServices>();
                services.AddTransient<ITestServices, TestServices>();
                services.AddTransient<IScoreServices, ScoreServices>();
                services.AddTransient<ILanguageServices, LanguageServices>();
                services.AddTransient<ITranscriptionServices, TranscriptionServices>();
                services.AddTransient<MediaServicesAlt>();
                services.AddTransient<TempServices>();
                services.AddTransient<WordServices>();


            }).Build();
            App.Current.Properties["AppHost"] = _host;
        }

        protected async override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            bool isAppRunningFirstTime;
            { 
                AppServices appServices = (AppServices)_host.Services.GetRequiredService<IAppServices>();
                isAppRunningFirstTime = appServices.isAppRunningFirstTime();
            }
            if (isAppRunningFirstTime)
            {
                firstTimeRunningSequence();
            }
            else
            {
                launchMainWindow();
                bool userRegistered = await IsUserRegistered();
                if (!userRegistered)
                {
                    registerUser();
                }
            }

            base.OnStartup(e);
        }

        private async void registerUser()
        {
            Guid guid;
            IUserServices userServices = _host.Services.GetRequiredService<IUserServices>();
            string userPublicId = await userServices.registerUser();
            if (Guid.TryParse(userPublicId, out guid))
            {
                await userServices.setPublicIdOfUser(userPublicId);
            }
        }

        private async Task<bool> IsUserRegistered()
        {
            IUserServices userServices = _host.Services.GetRequiredService<IUserServices>();
            return await userServices.checkIfCurrentUserRegistered();
        }

        public void launchMainWindow()
        {
            
            SetLanguageDictionary();
            //MainWindow window = new MainWindow() { DataContext = new MainViewModel(_navigationStore) };
            MainWindow window = _host.Services.GetRequiredService<MainWindow>();
            window.DataContext = _host.Services.GetRequiredService<MainViewModel>();
            window.Show();

        }

        private DbContextOptions getDatabaseOptions(DbContextOptionsBuilder optionBuilder)
        {
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
