
using LungoDatabase;
using LungoDatabase.Models;
using LungoModel.Models;
using LungoModel.Utils;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class ShowContextsWindow : Window
    {
        StorageContext _context;
        public ShowContextsWindow(StorageContext context)
        {
            _context = context;
            WordContext _wcontext;
            if(context.Context is TempWordContext)
            {
                TempWordContext tWC = (TempWordContext)context.Context;
                _wcontext = new WordContext()
                {
                    Address = tWC.Address,
                    Content = tWC.Content,
                    Type = tWC.Type
                };
            }
            else { _wcontext = (WordContext)context.Context; }
            context.Context = _wcontext;
            InitializeComponent();
            
            word.Text = context.Word;
            medium.Text = _wcontext.Type.ToString();
            source.Text = _wcontext.Address?.TranscriptionAddress.Title;
            timeStamp.Text = ((WordContext)(_context.Context)).Address?.SubLocation;
            setText();
            if (context.MediaLocation == null)
            {
                playButton.Visibility = Visibility.Hidden;
            }
        }


        private void setText()
        {
            string text_ = ((WordContext)(_context.Context)).Content;
            int index = text_.IndexOf(_context.Word, StringComparison.InvariantCultureIgnoreCase);
            
            if(index != -1)
            {
                text.Inlines.Add(new Run(text_.Substring(0, index)));
                text.Inlines.Add(new Bold(new Run(text_.Substring(index, _context.Word.Length))));
                text.Inlines.Add(new Run(text_.Substring(index + _context.Word.Length)));
            }
            else
            {
                text.Text = text_;
            }
            
        } 
        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Play(object sender, RoutedEventArgs e)
        {
            MediaTypes.TYPE tYPE = ((WordContext)_context.Context).Type;
            switch (tYPE)
            {
                case MediaTypes.TYPE.Youtube:
                    openYoutube();
                    break;
                case MediaTypes.TYPE.TVSeries:
                    openMedia();
                    break;
            }
        }

        private void openMedia()
        {
            MediaPlayer mediaPlayer = new MediaPlayer(_context);
            mediaPlayer.Show();
        }

        private void openYoutube()
        {
            string timeAppendix = getTimeAppendix(_context.Time);
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = _context.MediaLocation + timeAppendix,
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
            string meaning_word = Application.Current.Resources.MergedDictionaries[0]["ContextWindow_Meaning"].ToString();
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://www.google.com/search?q=" + wordStr + " " +  meaning_word,
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
