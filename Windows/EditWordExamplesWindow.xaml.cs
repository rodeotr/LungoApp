using LungoDatabase.DataAccess;
using LungoDatabase.Models;
using LungoDatabaseAccess.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
    public partial class EditWordExamplesWindow : Window
    {
        Word _word;
        public EditWordExamplesWindow(Word word)
        {
            _word = word;
            InitializeComponent();
            textBlock_wordName.Text = word.Name;
            listView_examples.ItemsSource = word.Example;


        }
        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void WordAddClick(object sender, RoutedEventArgs e)
        {
            if(textBox_addExample.Text.Length != 0)
            {
                IHost _hostApp = (IHost)App.Current.Properties["AppHost"];
                WordServices services = _hostApp.Services.GetRequiredService<WordServices>();
                int result = await services.addExample(_word, textBox_addExample.Text);
                if (result == 1)
                {
                    textBox_addExample.Text = "";
                    _word = await services.getWordByID(_word.Id);
                    listView_examples.ItemsSource = _word.Example;
                }
            }
        }
        private void MeaningEditClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void DifferentFormEditClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private async void DeleteExample(object sender, RoutedEventArgs e)
        {
            Example ex = (Example)((Button)sender).DataContext;
            IHost _hostApp = (IHost)App.Current.Properties["AppHost"];
            WordServices services = _hostApp.Services.GetRequiredService<WordServices>();
            int result = await services.deleteWordExample(ex.Id);
            if (result == 1)
            {
                _word = await services.getWordByID(_word.Id);
                listView_examples.ItemsSource = _word.Example;
            }
                
        }

    }
}

