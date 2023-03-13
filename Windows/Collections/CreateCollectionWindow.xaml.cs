using LungoDatabaseAccess;
using LungoDatabase.DataAccess;
using LungoDatabaseAccess.Services;
using LungoViewModels.ViewModels.Collections;
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
using LungoDatabase;

namespace LungoApp.Windows.Collections
{
    /// <summary>
    /// Interaction logic for ShowContextsWindow.xaml
    /// </summary>
    public partial class CreateCollectionWindow : Window
    {
        private TabCollectionsViewModel _vM;
        public CreateCollectionWindow(TabCollectionsViewModel vM)
        {
            InitializeComponent();
            _vM = vM;
            this.DataContext = this;
            collection.ItemsSource = Enum.GetValues(typeof(CollectionTypes.TYPE)).Cast<CollectionTypes.TYPE>();
            medium.ItemsSource = Enum.GetValues(typeof(MediaTypes.TYPE));


        }
        
        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private async void Create_Down(object sender, RoutedEventArgs e)
        {
            _vM.Model.createCollection(word.Text, collection.SelectedItem.ToString(),medium.SelectedItem.ToString());
            IHost _hostApp = (IHost)App.Current.Properties["AppHost"];
            ScoreServices services = _hostApp.Services.GetRequiredService<ScoreServices>();
            await services.IncrementScoreCollectionAdding();

            Close();
        }
    }
}
