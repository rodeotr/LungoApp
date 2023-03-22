//using LungDatabaseAccess.Services.IServices;
//using LungoApp.Windows;
//using LungoDatabase.DataAccess;
//using LungoDatabaseAccess.Services;
//using LungoDatabaseAccess.Services.Implementations;
//using LungoModels;
//using LungoViewModels.Stores;
//using LungoViewModels.ViewModels;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;

//namespace LungoApp
//{
//    internal class AppStart
//    {
//        public static IHost _host;
//        public async static Task<Window> Start(NavigationStore navigationStore)
//        {
//            InitializeHost(navigationStore);

//            _host.Start();
//            bool isAppRunningFirstTime;
//            {
//                AppServices appServices = (AppServices)_host.Services.GetRequiredService<IAppServices>();
//                isAppRunningFirstTime = appServices.isAppRunningFirstTime();
//            }
//            if (isAppRunningFirstTime)
//            {
//                return new FirstTimeSignUpWindow(
//                        _host.Services.GetRequiredService<IAppServices>(),
//                        _host.Services.GetRequiredService<ILanguageServices>()
//                    );
//            }
//            else
//            {
//                MainViewModel mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
//                mainViewModel.setViewModels(_host,_host.Services.GetRequiredService<DbContextOptions>());
//                bool userRegistered = await IsUserRegistered();
//                if (!userRegistered)
//                {
//                    registerUser();
//                }
//                Window mainWindow = new MainWindow();
//                mainWindow.DataContext = _host.Services.GetRequiredService<MainViewModel>();
//                return mainWindow;
                
//            }

//        }
//        private static void InitializeHost(NavigationStore navigationStore)
//        {
//            var _services = new ServiceCollection();
//            _services.AddSingleton(new LungoViewModels.IHostModel());

//            _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
//            {
//                services.AddSingleton(getDatabaseOptions());
//                services.AddDbContext<LungoContextDB>(options => getDatabaseOptions(options), ServiceLifetime.Transient);
//                services.AddSingleton(new WordProgressEventAggregator());
//                services.AddSingleton(new NewWordContextEventAggregator());
//                services.AddSingleton<NavigationStore>();
//                services.AddSingleton((s) => new MainViewModel(navigationStore));

//                services.AddTransient<IAppServices, AppServices>();
//                services.AddTransient<IUserServices, UserServices>();
//                services.AddTransient<ICollectionServices, CollectionServices>();
//                services.AddTransient<ITestServices, TestServices>();
//                services.AddTransient<IScoreServices, ScoreServices>();
//                services.AddTransient<ILanguageServices, LanguageServices>();
//                services.AddTransient<ITranscriptionServices, TranscriptionServices>();
//                services.AddTransient<MediaServicesAlt>();
//                services.AddTransient<TempServices>();
//                services.AddTransient<WordServices>();
//            }).Build();
//            App.Current.Properties["AppHost"] = _host;
//        }
//        private static DbContextOptions getDatabaseOptions(DbContextOptionsBuilder optionBuilder)
//        {
//            var builder = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

//            IConfigurationRoot configuration = builder.Build();
//            string connectionString = configuration.GetConnectionString("DefaultConnection");

//            optionBuilder.UseSqlServer(connectionString);
//            return optionBuilder.Options;
//        }

//        private static DbContextOptions getDatabaseOptions()
//        {
//            var builder = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

//            IConfigurationRoot configuration = builder.Build();
//            string connectionString = configuration.GetConnectionString("DefaultConnection");

//            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
//            optionsBuilder.UseSqlServer(connectionString);
//            return optionsBuilder.Options;
//        }

//        //private void firstTimeRunningSequence()
//        //{
//        //    AppServices appServices = (AppServices)_host.Services.GetRequiredService<IAppServices>();
//        //    //appServices.addAllLanguagesToDB();
//        //    FirstTimeSignUpWindow firstTimeSignUp = new FirstTimeSignUpWindow(this);
//        //    firstTimeSignUp.Show();
//        //}

//        private static async void registerUser()
//        {
//            Guid guid;
//            IUserServices userServices = _host.Services.GetRequiredService<IUserServices>();
//            string userPublicId = await userServices.registerUser();
//            if (Guid.TryParse(userPublicId, out guid))
//            {
//                await userServices.setPublicIdOfUser(userPublicId);
//            }
//        }

//        private static async Task<bool> IsUserRegistered()
//        {
//            IUserServices userServices = _host.Services.GetRequiredService<IUserServices>();
//            return await userServices.checkIfCurrentUserRegistered();
//        }
//    }
//}
