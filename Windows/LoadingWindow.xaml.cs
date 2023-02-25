﻿using LungoModels;
using LungoViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LungoApp.Windows
{
    /// <summary>
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        private readonly WordProgressEventAggregator _eventAggregator;
        private WordProgressMessage _message;
        private LoadingContext _loadingContext;
        Random r;
        int num;

        public LoadingWindow(WordProgressEventAggregator eventAggregator)
        {
            InitializeComponent();
            _eventAggregator = eventAggregator;
            _loadingContext = new LoadingContext();
            DataContext = _loadingContext;
            r = new Random();

            _eventAggregator.ShowWindowObservable.Subscribe(x => { OnShowWindow(x); });
        }

        public void OnShowWindow(WordProgressMessage message)
        {
            if (!message.shouldClose)
            {
                _message = message;
                num = r.Next(100);
                if (num < 8)
                {
                    Dispatcher.Invoke(new Action(() => { updateUI(message.wordName); }), DispatcherPriority.DataBind);

                }

                //Dispatcher.Invoke(() => {
                //    progressText.Text = message.wordName;
                //    progressBar.Value = Int32.Parse(message.percentage);
                //    Console.WriteLine(progressText.Text);
                //});
            }
        }

        private void updateUI(string word)
        {
            _loadingContext.WordName = word;
            _loadingContext.Percentage = _message.percentage;
            Console.WriteLine(_loadingContext.WordName);
            //progressText.Text = _message.wordName;
            //progressBar.Value = Int32.Parse(_message.percentage);
        }

        public class LoadingContext : INotifyPropertyChanged
        {
            private string wordName;
            private string percentage;

            public string WordName { get => wordName; set { wordName = value;OnPropertyChanged(nameof(WordName)); } }
            public string Percentage { get => percentage; set { percentage = value; OnPropertyChanged(nameof(Percentage)); } }

            public event PropertyChangedEventHandler? PropertyChanged;
            public void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
