
using LungoDatabase.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LungoApp.Windows
{
    /// <summary>
    /// Interaction logic for ShowContextsWindow.xaml
    /// </summary>
    public partial class ShowExampleWindow : Window
    {
        Example _example;
        public ShowExampleWindow(Example example)
        {
            InitializeComponent();
            _example = example;
            textBlock_example.Text = example.Text;
            textBlock_initTime.Text = example.InitTime.ToString();
            textBlock_source.Text = example.Source;
        }

     
        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
      
    }
}
