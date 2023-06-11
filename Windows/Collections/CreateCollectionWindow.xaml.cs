using LungoDatabaseAccess;
using LungoDatabase.DataAccess;
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
using LungDatabaseAccess.Services.IServices;
using LungoDatabaseAccess.Services.Implementations;
using LungoModel.Exceptions;
using LungoModel.Interfaces;

namespace LungoApp.Windows.Collections
{
    /// <summary>
    /// Interaction logic for ShowContextsWindow.xaml
    /// </summary>
    public partial class CreateCollectionWindow : Window
    {
        private TabCollectionsViewModel _vM;
        private ContextClosable _cC;
        public CreateCollectionWindow(TabCollectionsViewModel vM, ContextClosable cC)
        {
            InitializeComponent();
            _vM = vM;
            _cC = cC;
            this.DataContext = this;
            collection.ItemsSource = Enum.GetValues(typeof(CollectionTypes.TYPE)).Cast<CollectionTypes.TYPE>();
            medium.ItemsSource = Enum.GetValues(typeof(MediaTypes.TYPE));
        }
        
        private void Close(object sender, RoutedEventArgs e)
        {
            _cC.OpenContexts.Remove("CreateCollection");
            Close();
        }
        private async void Create_Down(object sender, RoutedEventArgs e)
        {
            try
            {
                await _vM.Model.createCollection(word.Text, collection.SelectedItem.ToString(), medium.SelectedItem.ToString());
                IHost _hostApp = (IHost)App.Current.Properties["AppHost"];
                IScoreServices services = _hostApp.Services.GetRequiredService<IScoreServices>();
                services.IncrementScore(ScoreIncrements.PointsCollection);
                _vM.OnPropertyChanged(nameof(_vM.CollectionList));
            }
            catch (CollectionCreationException ex)
            {
                switch (ex.Reason)
                {
                    case "NameExists":
                        MessageBox.Show("A collection with this name already exists.");
                        break;
                }
            }

            _cC.OpenContexts.Remove("CreateCollection");
            Close();
        }
    }
}
