using LungoApp.Windows.Collections;
using LungoModels;
using LungoViewModels;
using LungoViewModels.ViewModels.Learn;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace LungoApp.Views.Learn
{
    /// <summary>
    /// Interaction logic for tabListWords.xaml
    /// </summary>
    /// 
    
    public partial class tabListWordsView : UserControl
    {
        ScrollViewer scroll;
        private NewWordContextEventAggregator _eventAggregator;
        private TabLearnNewWordsViewModel _viewModel;
        int _lowerValue = 1;
        int _higherValue = 2;
        public tabListWordsView()
        {
            InitializeComponent();
            IHost _hostApp = (IHost)App.Current.Properties["AppHost"];
            _eventAggregator = _hostApp.Services.GetRequiredService<NewWordContextEventAggregator>();
            _eventAggregator.ShowWindowObservable.Subscribe(x => { launchContextWindow(x); });
            
        }

        private void launchContextWindow(NewWordContextMessage Message)
        {
            ShowContextsWindow contextsWindow = new ShowContextsWindow(Message.Context);
            contextsWindow.Show();
        }

        private void WordFrequencyRangeLowerValueChanged(object sender, RoutedEventArgs e)
        {
            _lowerValue = (int)((RangeSlider)sender).LowerValue;
            _viewModel = (TabLearnNewWordsViewModel)DataContext;
            if (_viewModel != null)
            {
                _viewModel.MembersModel.setAllPagesByFrequency(_lowerValue, _higherValue);
                _viewModel.OnPropertyChanged(nameof(_viewModel.Members));
                _viewModel.OnPropertyChanged(nameof(_viewModel.TotalWordString));
            }
            
        }
        private void WordFrequencyRangeHigherValueChanged(object sender, RoutedEventArgs e)
        {
            _higherValue = (int)((RangeSlider)sender).HigherValue;
            _viewModel = (TabLearnNewWordsViewModel)DataContext;
            if (_viewModel != null)
            {
                _viewModel.MembersModel.setAllPagesByFrequency(_lowerValue, _higherValue);
                _viewModel.OnPropertyChanged(nameof(_viewModel.Members));
                _viewModel.OnPropertyChanged(nameof(_viewModel.TotalWordString));
            }
        }






        public static ScrollViewer GetScrollViewer(UIElement element)
        {
            if (element == null) return null;

            ScrollViewer retour = null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element) && retour == null; i++)
            {
                if (VisualTreeHelper.GetChild(element, i) is ScrollViewer)
                {
                    retour = (ScrollViewer)(VisualTreeHelper.GetChild(element, i));
                }
                else
                {
                    retour = GetScrollViewer(VisualTreeHelper.GetChild(element, i) as UIElement);
                }
            }
            return retour;
        }
        public void pageClicked(object sender, RoutedEventArgs e)
        {
            if(scroll == null)
            {
                scroll = GetScrollViewer(membersDataGrid);
            }
            scroll.ScrollToTop();
        }



    }
}
