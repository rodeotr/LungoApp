using LungDatabaseAccess.Services.IServices;
using LungoDatabaseAccess.Services.Implementations;
using LungoModel.Interfaces;
using LungoModel.Models;
using LungoModel.Utils;
using LungoViewModels.ViewModels.Browse;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        CollectionModelTemplate _collection;
        private readonly ContextClosable _vM;

        public EditCollectionWindow(CollectionModelTemplate collection, ContextClosable vM)
        {
            InitializeComponent();
            _collection = collection;
            _vM = vM;
            this.DataContext = collection;
           
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            _vM.OpenContexts.Remove(_collection.Name);
            Close();
        }
        private async void PublishToServer(object sender, RoutedEventArgs e)
        {
            await ServerUtils.publishCollectionToServer(_collection.Name);
        }
        private void DeleteCollection(object sender, RoutedEventArgs e)
        {
            IHost _hostApp = (IHost)App.Current.Properties["AppHost"];
            CollectionServices services = (CollectionServices)_hostApp.Services.GetRequiredService<ICollectionServices>();
            services.DeleteCollection(_collection.DBId);
            _vM.OpenContexts.Remove(_collection.Name);
            Close();
        }

    }
}

