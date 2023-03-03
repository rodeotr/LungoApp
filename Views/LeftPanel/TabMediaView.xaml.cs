using LungoDatabase.Models;
using LungoModel.Models;
using LungoViewModels.ViewModels.Media;
using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LungoApp.Views.LeftPanel
{
    /// <summary>
    /// Interaction logic for TabMediaView.xaml
    /// </summary>
    public partial class TabMediaView : UserControl
    {
        public TabMediaView()
        {
            InitializeComponent();
        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            { 
                //txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
                string myValue = ((Button)sender).Tag.ToString();
                ((MenuMediaViewModel)(this.DataContext)).SelectedIndex = myValue;
                ((MenuMediaViewModel)(this.DataContext)).MediaLocation = openFileDialog.FileName;
            }
        }

        public async void DataGridRow_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            DataGridRow dataGridRow = (DataGridRow)sender;
            ((MenuMediaViewModel)DataContext).setTheWordsOfMedia((MediaMember)dataGridRow.DataContext);
            Console.WriteLine("");
        }
    }
}
