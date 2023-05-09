
using LungoApp.Windows.Collections;
using LungoModel.Models;
using LungoViewModels.ViewModels.Collections;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LungoApp.Views.Collections
{
    /// <summary>
    /// Interaction logic for tabListWords.xaml
    /// </summary>
    public partial class TabCollectionsView : UserControl
    {
        CreateCollectionWindow createCollectionwindow;
        EditCollectionWindow editCollectionWindow;
        public TabCollectionsView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ((TabCollectionsViewModel)DataContext)._contextCreationEvent += (vM) => CreateCollection(vM);
            ((TabCollectionsViewModel)DataContext)._collectionEditEvent += (cModel) => EditCollection(cModel);
        }

        private void EditCollection(CollectionModelTemplate cModel)
        {
            editCollectionWindow = new EditCollectionWindow(cModel);
            editCollectionWindow.Show();
        }

        private void CreateCollection(TabCollectionsViewModel vM)
        {
            if (createCollectionwindow == null)
            {
                createCollectionwindow = new CreateCollectionWindow(vM);
                createCollectionwindow.Show();
            }
            else if(!createCollectionwindow.IsVisible)
            {
                createCollectionwindow = new CreateCollectionWindow(vM);
                createCollectionwindow.Show();
            }
        }
    }
}
