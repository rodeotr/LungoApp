using CaptionCrafter;
using LungoApp.Windows;
using LungoModels;
using LungoViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LungoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IDisposable subscription;
        private WordProgressEventAggregator eventAggregator;
        private LoadingWindow _loadingWindow;

        public MainWindow()
        {
            InitializeComponent();
            IHost _hostApp = (IHost)App.Current.Properties["AppHost"];
            eventAggregator = _hostApp.Services.GetRequiredService<WordProgressEventAggregator>();
            subscription = eventAggregator.ShowWindowObservable.Subscribe(x => { Update(x); });


        }

        private async void Update(Report a)
        {
            if(_loadingWindow == null)
            {
                Dispatcher.Invoke(() =>
                {
                    _loadingWindow = new LoadingWindow(eventAggregator);
                    _loadingWindow.Show();
                });
                
            }
            else if (a.EndOfProcess)
            {
                Dispatcher.Invoke(() =>
                {
                    subscription.Dispose();
                    _loadingWindow.Close();
                    _loadingWindow = null;
                });
                
            }
            else
            {
                //await this.Dispatcher.BeginInvoke(new Action(() => { _loadingWindow.progressText.Text = a.wordName; }), DispatcherPriority.Normal);

                //Dispatcher.Invoke(() => { 
                //_loadingWindow.progressText.Text = a.wordName;
                //});
            }
            
            
        }

        private void ExitApplication(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
