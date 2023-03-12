using LungoViewModels.ViewModels.Browse;
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
    public partial class TabBrowseMainView : UserControl
    {
        public TabBrowseMainView()
        {
            InitializeComponent();
            Loaded += OnLoaded;

        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            TabBrowseViewModel vM = (TabBrowseViewModel)DataContext;
            vM.LoadData();
            
        }

        
    }
}
