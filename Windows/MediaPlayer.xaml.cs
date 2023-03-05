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
        private bool _isPlaying = false;

        public MediaPlayer(StorageContext context)
        {
            _context = context;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            Height = screenHeight > 700 ? 700 : screenHeight;
            Width = screenHeight == 700 ? 1244 : screenHeight * (16/9);

            InitializeComponent();
            mediaPlayer.Source = new Uri(((WordContext)(context.Context)).Address.TranscriptionAddress.MediaLocation);
            mediaPlayer.Position = setPosition();
            
            mediaPlayer.Play();
            _isPlaying = true;
        }

        private TimeSpan setPosition()
        {
            string position = ((WordContext)(_context.Context)).Address.SubLocation;
            int hour = Int32.Parse(position.Substring(0, 2));
            int minutes = Int32.Parse(position.Substring(3, 2));
            int seconds = Int32.Parse(position.Substring(6, 2));
            return (TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minutes) + TimeSpan.FromSeconds(seconds));
        }

        private void PlayOrPause(object sender, EventArgs e)
        {
            if (_isPlaying)
            {
                mediaPlayer.Pause();
                playPauseIcon.Kind =  MahApps.Metro.IconPacks.PackIconMaterialKind.Play;
                _isPlaying = false;
            }
            else
            {
                mediaPlayer.Play();
                playPauseIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.Pause;
                _isPlaying = true;
            }
        }
        private void Forward10Sec(object sender, EventArgs e)
        {
            mediaPlayer.Position += TimeSpan.FromSeconds(10);
        }
        private void Reverse10Sec(object sender, EventArgs e)
        {
            mediaPlayer.Position -= TimeSpan.FromSeconds(10);
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space:
                    PlayOrPause(sender, e);
                    break;
                case Key.Escape:
                    Close();
                    break;
                case Key.Right:
                    Forward10Sec(sender, e);
                    break;
                case Key.Left:
                    Reverse10Sec(sender, e);
                    break;
            }
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
