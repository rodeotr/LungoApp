using LungDatabaseAccess.Services.IServices;
using LungoDatabase.DataAccess;
using LungoDatabase.Models;
using LungoDatabaseAccess.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
        private List<Language> _languages;
        public FirstTimeSignUpWindow(App app)
        {
            InitializeComponent();
            _app = app;
            setLanguageSources();

            ResourceDictionary dict = new ResourceDictionary()
            {
                Source = new Uri("..\\ResourceDictionary\\LangEnglish.xaml", UriKind.Relative)
            };
            Resources.MergedDictionaries.Add(dict);
        }

        private void setLanguageDictionary(string language)
        {
            Language lang = _languages.First(a => a.Name.Equals(language));
            if(lang.AppLanguageRepositoryFileName == null || lang.AppLanguageRepositoryFileName.Length == 0)
            {
                return;
            }

            Resources.MergedDictionaries.RemoveAt(0);
            string url = "..\\ResourceDictionary\\" + lang.AppLanguageRepositoryFileName + ".xaml";
            ResourceDictionary dict = new ResourceDictionary()
            {
                Source = new Uri(url, UriKind.Relative)
            };
            Resources.MergedDictionaries.Add(dict);

        }

        private void setLanguageSources()
        {
            IHost _hostApp = (IHost)App.Current.Properties["AppHost"];
            DbContextOptions options = _hostApp.Services.GetRequiredService<DbContextOptions>();
            using (LungoContextDB context = new LungoContextDB(options))
            {
                SettingServices services = new SettingServices(context);
                _languages = services.getAllLanguages();
                string[] languageArray = _languages.Select(a => a.Name).ToArray();
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
                    Languages = new List<Language>() { toLearnLanguage},
                    Gender = gender,
                    Score = new Score(),
                    InitTime = DateTime.Now
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
        private void YourLanguageChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            string selected = combo.SelectedItem.ToString();

            setLanguageDictionary(selected);
        }
    }
    

}
