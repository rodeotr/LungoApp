
using LungoApp.Windows.Collections;
using LungoModel.Models;
using LungoViewModels.ViewModels.Storage;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LungoApp.Views.LeftPanel
{
    /// <summary>
    /// Interaction logic for tabListWords.xaml
    /// </summary>
    public partial class TabStorageWordsView : UserControl
    {
        TabStorageWordsViewModel _vm;
        public TabStorageWordsView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _vm = (TabStorageWordsViewModel)DataContext;
            _vm.StorageShowtEvent += (context) => ShowContext(context);
        }

        private void ShowContext(StorageContext context)
        {
            ShowContextsWindow window = new ShowContextsWindow(context);
            window.Show();
        }

       
    
    
    }
}
