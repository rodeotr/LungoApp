using LungoApp.Windows.Collections;
using LungoModel.Models;
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
    public partial class TabTestView : UserControl
    {
        public TabTestView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            TabTestViewModel vM = (TabTestViewModel)DataContext;
            vM.TestContextEvent += (context) => ShowContext(context);
        }

        private void ShowContext(StorageContext context)
        {
            ShowContextsWindow window = new ShowContextsWindow(context);
            window.Show();
        }
    }
}
