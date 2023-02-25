using LungoApp.Windows.Collections;
using LungoModels;
using LungoViewModels;
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

namespace LungoApp.Views.Learn
{
    /// <summary>
    /// Interaction logic for tabListWords.xaml
    /// </summary>
    public partial class tabListWordsView : UserControl
    {
        ScrollViewer scroll;
        private NewWordContextEventAggregator eventAggregator;
        public tabListWordsView()
        {
            InitializeComponent();
            IHost _hostApp = (IHost)App.Current.Properties["AppHost"];
            eventAggregator = _hostApp.Services.GetRequiredService<NewWordContextEventAggregator>();
            eventAggregator.ShowWindowObservable.Subscribe(x => { launchContextWindow(x); });
        }

        private void launchContextWindow(NewWordContextMessage Message)
        {
            ShowContextsWindow contextsWindow = new ShowContextsWindow(Message.Context);
            contextsWindow.Show();
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
