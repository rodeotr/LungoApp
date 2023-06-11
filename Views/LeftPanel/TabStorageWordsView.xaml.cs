
using LungoApp.Windows;
using LungoApp.Windows.Collections;
using LungoDatabase.Models;
using LungoModel.Interfaces;
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
    public partial class TabStorageWordsView : UserControl, ContextClosable
    {
        TabStorageWordsViewModel _vm;


        public TabStorageWordsView()
        {
            InitializeComponent();
            OpenContexts = new List<string>();
            Loaded += OnLoaded;
            
        }
        public List<string> OpenContexts { get; set; }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _vm = (TabStorageWordsViewModel)DataContext;
            _vm.StorageShowEvent += (context) => ShowContext(context);
            _vm.WordEditEvent += (member) => ShowWordEdit(member);
            _vm.AddToCollectionEvent += (member, collections) => AddToCollection(member, collections);
        }

        private void ShowContext(StorageContext context)
        {
            if (!OpenContexts.Contains(context.Word))
            {
                OpenContexts.Add(context.Word);
                ShowContextsWindow window = new ShowContextsWindow(context);
                window.parentWindow = this;
                window.Show();
            }
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
            _vm.CurrentMembers = new ObservableCollection<WordMember>(_vm.StorageMemberModel.AllMembers.Where(a => a.Name.Contains(text)).ToList());
        } 




    }
}
