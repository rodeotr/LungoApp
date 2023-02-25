using LungoModel.Utils;
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
using System.Windows.Shapes;

namespace LungoApp.Windows.Collections
{
    /// <summary>
    /// Interaction logic for ShowContextsWindow.xaml
    /// </summary>
    public partial class EditCollectionWindow : Window
    {
        CollectionModel _collection;
        public EditCollectionWindow(CollectionModel collection)
        {
            InitializeComponent();
            _collection = collection;
            this.DataContext = collection;
           
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void PublishToServer(object sender, RoutedEventArgs e)
        {
            ServerUtils.publishCollectionToServer(_collection.Name);
        }

    }
}

