
using LungoApp.Windows;
using LungoApp.Windows.Collections;
using LungoDatabase.Models;
using LungoModel.Models;
using LungoViewModels.ViewModels.Storage;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            _vm.WordEditEvent += (member) => ShowWordEdit(member);
            _vm.AddToCollectionEvent += (member, collections) => AddToCollection(member, collections);
        }

        private void ShowContext(StorageContext context)
        {
            ShowContextsWindow window = new ShowContextsWindow(context);
            window.Show();
        }
        private void ShowWordEdit(WordMember member)
        {
            EditWordWindow window = new EditWordWindow(member);
            window.Show();
        }
        private void AddToCollection(WordMember member, List<Collection> collections)
        {
            AddToCollectionWindow window = new AddToCollectionWindow(member, collections);
            window.Show();
        }

        private void SearchChanged(object sender, TextChangedEventArgs args)
        {
            string text = ((TextBox)sender).Text;
            if(text.Length == 0)
            {
                _vm.StorageMemberModel.updateCurrentMembers();
            }
            else
            {
                _vm.CurrentMembers = new ObservableCollection<WordMember>(_vm.StorageMemberModel.AllMembers.Where(a => a.Name.Contains(text)).ToList());
            }
            _vm.OnPropertyChanged(nameof(_vm.CurrentMembers));

        } 




    }
}
