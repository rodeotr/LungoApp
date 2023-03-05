using LungoDatabase.Models;
using LungoModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for MediaPlayer.xaml
    /// </summary>
    public partial class MediaPlayer : Window
    {
        private readonly StorageContext _context;

        public MediaPlayer(StorageContext context)
        {
            _context = context;
            InitializeComponent();
            mediaPlayer.Source = new Uri(((WordContext)(context.Context)).Address.TranscriptionAddress.MediaLocation);
            mediaPlayer.Position = setPosition();
            
            mediaPlayer.Play();
        }

        private TimeSpan setPosition()
        {
            string position = ((WordContext)(_context.Context)).Address.SubLocation;
            int hour = Int32.Parse(position.Substring(0, 2));
            int minutes = Int32.Parse(position.Substring(3, 2));
            int seconds = Int32.Parse(position.Substring(6, 2));
            return (TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minutes) + TimeSpan.FromSeconds(seconds));
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            mediaPlayer.Stop();
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
