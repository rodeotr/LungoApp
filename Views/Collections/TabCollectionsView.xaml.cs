
using LungoApp.Windows.Collections;
using LungoModel.Interfaces;
using LungoModel.Models;
using LungoViewModels.ViewModels.Collections;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LungoApp.Views.Collections
{
    /// <summary>
    /// Interaction logic for tabListWords.xaml
    /// </summary>
    public partial class TabCollectionsView : UserControl, ContextClosable
    {
        CreateCollectionWindow createCollectionwindow;
        EditCollectionWindow editCollectionWindow;

        public List<string> OpenContexts { get; set; }

        public TabCollectionsView()
        {
            InitializeComponent();
            OpenContexts = new List<string>();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ((TabCollectionsViewModel)DataContext)._contextCreationEvent += (vM) => CreateCollection(vM);
            ((TabCollectionsViewModel)DataContext)._collectionEditEvent += (cModel) => EditCollection(cModel);
        }

        private void EditCollection(CollectionModelTemplate cModel)
        {
            if (!OpenContexts.Contains(cModel.Name))
            {
                OpenContexts.Add(cModel.Name);
                editCollectionWindow = new EditCollectionWindow(cModel, this);
                editCollectionWindow.Show();
            }
            
        }

        private void CreateCollection(TabCollectionsViewModel vM)
        {
            
            if (!OpenContexts.Contains("CreateCollection"))
            {
                OpenContexts.Add("CreateCollection");
                createCollectionwindow = new CreateCollectionWindow(vM, this);
                createCollectionwindow.Show();
            }
        }
    }
}
