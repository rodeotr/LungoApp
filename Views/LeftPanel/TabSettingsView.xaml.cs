using LungoApp.Windows;
using LungoViewModels.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LungoApp.Views.LeftPanel
{
    /// <summary>
    /// Interaction logic for tabDashBoard.xaml
    /// </summary>
    public partial class TabSettingsView : UserControl
    {
        public TabSettingsView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ((MenuSettingsViewModel)DataContext).AddLanguageEvent += (languageList, vM) => ShowAddLanguage(languageList, vM);
        }

        private void ShowAddLanguage(string[] languageList, MenuSettingsViewModel vM)
        {
            AddLanguageWindow window = new AddLanguageWindow(languageList, vM);
            window.Show();
        }
    }
}
