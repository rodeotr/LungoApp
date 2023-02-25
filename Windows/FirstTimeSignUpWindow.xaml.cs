using LungDatabaseAccess.Services.IServices;
using LungoDatabase.DataAccess;
using LungoDatabase.Models;
using LungoDatabaseAccess.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LungoApp.Windows
{
    /// <summary>
    /// Interaction logic for FirstTimeSignUpWindow.xaml
    /// </summary>
    public partial class FirstTimeSignUpWindow : Window
    {
        private readonly App _app;
        private bool _maleActive = true;
        public FirstTimeSignUpWindow(App app)
        {
            InitializeComponent();
            _app = app;
            setLanguageSources();
        }

        private void setLanguageSources()
        {
            IHost _hostApp = (IHost)App.Current.Properties["AppHost"];
            DbContextOptions options = _hostApp.Services.GetRequiredService<DbContextOptions>();
            using (LungoContextDB context = new LungoContextDB(options))
            {
                SettingServices services = new SettingServices(context);
                List<Language> languages = services.getAllLanguages();
                string[] languageArray = languages.Select(a => a.Name).ToArray();
                LanguageComboBox.ItemsSource = languageArray;
                LanguageToLearnComboBox.ItemsSource = languageArray;
            }
            
        }

        private void maleClicked(object sender, MouseButtonEventArgs e)
        {
            if (!_maleActive)
            {
                maleAvatar.Opacity = 1;
                femaleAvatar.Opacity = 0.5;
                _maleActive = true;
            }
        }

        private void femaleClicked(object sender, MouseButtonEventArgs e)
        {
            if (_maleActive)
            {
                maleAvatar.Opacity = 0.5;
                femaleAvatar.Opacity = 1;
                _maleActive = false;
            }
        }

        private async void Save_MouseDowned(object sender, MouseButtonEventArgs e)
        {
            if(NameTextBox.Text.Length == 0)
            {
                MessageBox.Show("Please Enter Your Name");
                return;
            }

            string gender = _maleActive ? "M" : "F";
            IHost host = (IHost)App.Current.Properties["AppHost"];
            IAppServices appServices = (IAppServices)host.Services.GetRequiredService<IAppServices>();

            IHost _hostApp = (IHost)App.Current.Properties["AppHost"];
            DbContextOptions options = _hostApp.Services.GetRequiredService<DbContextOptions>();
            using (LungoContextDB context = new LungoContextDB(options))
            {
                SettingServices services = new SettingServices(context);
                Language mainLanguage = await services.getLanguageByString(LanguageComboBox.SelectedItem.ToString());
                Language toLearnLanguage = await services.getLanguageByString(LanguageToLearnComboBox.SelectedItem.ToString());

                User user = new User()
                {
                    Name = NameTextBox.Text,
                    MotherLanguage = mainLanguage,
                    CurrentLanguage = toLearnLanguage,
                    Gender = gender,
                    Score = new Score()
                };
                await appServices.addUserToDB(user);
                await appServices.SetCurrentUser(await services.getFirstUser());

                _app.launchMainWindow();
                Close();
            }

               
           



            //appServices.initializeFirstTimeObjects(
            //    NameTextBox.Text,
            //    LanguageComboBox.SelectedItem.ToString(),
            //    LanguageToLearnComboBox.SelectedItem.ToString(),
            //    gender);
            
        }
    }
    

}
