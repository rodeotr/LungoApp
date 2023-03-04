﻿
using LungoApp.Windows.Collections;
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
        public TabCollectionsView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ((TabCollectionsViewModel)DataContext)._contextCreationEvent += (vM) => CreateCollection(vM);
        }

        private void CreateCollection(TabCollectionsViewModel vM)
        {
            CreateCollectionWindow window = new CreateCollectionWindow(vM);
            window.Show();
        }
    }
}
