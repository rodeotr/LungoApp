using LungoViewModels.ViewModels.Learn.Tabs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for tabAddMedia.xaml
    /// </summary>
    public partial class tabAddMediaView : UserControl
    {
        private Regex _regex;
        public tabAddMediaView()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            if(((MouseButtonEventArgs)e).ClickCount == 1)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.DefaultExt = ".srt";
                if (openFileDialog.ShowDialog() == true)
                    //txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
                    ((TabAddMediaViewModel)(this.DataContext)).TranscriptionLocation = openFileDialog.FileName;
                e.Handled = true;
            }
            
        }
        //private void Click_Run(object sender, RoutedEventArgs e)
        //{
        //    Console.WriteLine(LinkTextBox.Text);

        //}
        private void PreviewTextInputMaxFreq(object sender, TextCompositionEventArgs e)
        {
            //Console.WriteLine(e.Text);
            e.Handled = !IsTextAllowed(e.Text);
        }
        private bool IsTextAllowed(string text)
        {
            _regex = new Regex("[^0-9.-]+");
            return !_regex.IsMatch(text);
        }
    }
}
