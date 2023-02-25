using LungoDatabase;
using LungoDatabaseAccess;
using LungoViewModels.ViewModels.Collections;
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

namespace LungoApp.Windows.Collections
{
    /// <summary>
    /// Interaction logic for ShowContextsWindow.xaml
    /// </summary>
    public partial class ShowCollectionContextsWindow : Window
    {
        CollectionItemContext _context;

        public object LangDataAccessLibrary { get; }

        public ShowCollectionContextsWindow(CollectionItemContext context)
        {
            InitializeComponent();
            _context = context;
            word.Text = context.Word;
            medium.Text = context.MediaType.ToString();
            text.Text = context.Text;

            if(context.MediaType != MediaTypes.TYPE.Youtube)
            {
                playButton.Visibility = Visibility.Hidden;
            }
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Play(object sender, RoutedEventArgs e)
        {
            string timeAppendix = getTimeAppendix(_context.Address.SubLocation);
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                //FileName = "https://www.google.com/search?q=" + wordStr + " meaning",
                FileName = _context.Address.Text + timeAppendix,
                UseShellExecute = true
            });
        }

        private void showImage(object sender, RoutedEventArgs e)
        {
            string wordStr = word.Text;
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://www.google.com/search?hl=en&site=imghp&tbm=isch&source=hp&q=" + wordStr,
                UseShellExecute = true
            });
        }

        private void getMeaning(object sender, RoutedEventArgs e)
        {
            string wordStr = word.Text;
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://www.google.com/search?q=" + wordStr + " meaning",
                UseShellExecute = true
            });
        }

        private string getTimeAppendix(string subLocation)
        {
            string prefix = "&t=";
            int hour = Int32.Parse(subLocation.Substring(0, 2));
            int minute = Int32.Parse(subLocation.Substring(3, 2));
            int second = Int32.Parse(subLocation.Substring(6, 2));

            second -= 3;
            if (second < 0)
            {
                minute -= 1;
                second += 60;
            }
            string time = prefix + ((hour * 60) + minute).ToString() + "m" + second.ToString() + "s";

            return time;
        }
    }
}
