using LungDatabaseAccess.Services.IServices;
using LungDatabaseAccess.Services;
using LungoModel.Models;
using LungoViewModels.ViewModels;
using LungoViewModels.ViewModels.Storage;
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
using Microsoft.EntityFrameworkCore;
using LungoDatabase.DataAccess;
using LungoDatabase.Models;
using LungoViewModels.ViewModels.Collections;
using LungoDatabaseAccess.Services.Implementations;

namespace LungoApp.Windows
{
    /// <summary>
    /// Interaction logic for ShowContextsWindow.xaml
    /// </summary>
    public partial class AddToCollectionWindow : Window
    {
        WordMember _wm;
        public AddToCollectionWindow(WordMember wM, List<Collection> collections)
        {
            InitializeComponent();
            _wm = wM;
            this.DataContext = this;
            collection.ItemsSource = collections.Select(s=>s.Name).ToList();


        }
            
        private async void AddButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(collection.Text.Length != 0)
            {
                int result = 0;
                IHost _hostApp = (IHost)App.Current.Properties["AppHost"];
                CollectionServices services = _hostApp.Services.GetRequiredService<CollectionServices>();
                result = await services.addWordToCollection(collection.SelectedItem.ToString(), _wm.Name);

                if (result == -1)
                {
                    MessageBox.Show("Collection Type Mismatch.");
                }
                else{
                    // FIX THIS LATER. UPDATING REQUIRED FIELDS

                    //IHost _hostMain = (IHost)App.Current.Properties["MainViewModelHost"];
                    //MenuStorageMainViewModel vM = _hostMain.Services.GetRequiredService<MenuStorageMainViewModel>();
                    //MenuCollectionsMainViewModel vM_collections = _hostMain.Services.GetRequiredService<MenuCollectionsMainViewModel>();
                    //vM_collections.TabcollectionsViewModel.updateTheFields();
                    //vM.TabStorageWordsViewModel.Refresh();
                    //vM.TabStorageWordsViewModel.raisePropertyChangedEvent(nameof(vM.TabStorageWordsViewModel.CurrentMembers));
                }
            }
            Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
     
    }
}
