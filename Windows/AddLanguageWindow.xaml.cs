using LungDatabaseAccess.Services.IServices;
using LungoDatabase.DataAccess;
using LungoDatabase.Models;
using LungoDatabaseAccess.Services.Implementations;
using LungoViewModels.ViewModels.Settings;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LungoApp.Windows
{
    /// <summary>
    /// Interaction logic for ShowContextsWindow.xaml
    /// </summary>
    public partial class AddLanguageWindow : Window
    {
        MenuSettingsViewModel _vM;
        public AddLanguageWindow(string[] _languages,MenuSettingsViewModel vM)
        {
            InitializeComponent();
            _vM = vM;
            comboLanguages.ItemsSource = _languages;
            this.DataContext = this;



        }

        private async void AddButtonDown(object sender, MouseButtonEventArgs e)
        {
            IHost _hostApp = (IHost)App.Current.Properties["AppHost"];
            ILanguageServices languageServices = _hostApp.Services.GetRequiredService<ILanguageServices>();
            IScoreServices servicesScore = _hostApp.Services.GetRequiredService<IScoreServices>();
            Language language = new Language() { Name = comboLanguages.SelectedItem.ToString() };
            int result = await languageServices.AddLanguageToCurrentUser(language);
            _vM.updateTheFields();
            await servicesScore.IncrementScore(ScoreIncrements.PointsLanguage);
            Close();

                
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
     
    }
}
