using LungoViewModels.ViewModels.Test;
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
    /// Interaction logic for tabTest.xaml
    /// </summary>
    public partial class TabTestOverview : UserControl
    {
        public TabTestOverview()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            BeginAnim();
            
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            MenuTestDashViewModel vM = (MenuTestDashViewModel)DataContext;
            vM.Model.updateWords();
        }

        private void BeginAnim()
        {
            
        }
    }
}
