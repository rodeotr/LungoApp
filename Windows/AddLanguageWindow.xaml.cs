using LungoDatabase.DataAccess;
using LungoDatabaseAccess.Services;
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
            SettingServices services = _hostApp.Services.GetRequiredService<SettingServices>();
            ScoreServices servicesScore = _hostApp.Services.GetRequiredService<ScoreServices>();
            await services.AddLanguageToCurrentUser(comboLanguages.SelectedItem.ToString());
            _vM.updateTheFields();
            await servicesScore.IncrementScoreLanguageAdding();
            Close();

                
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
     
    }
}
