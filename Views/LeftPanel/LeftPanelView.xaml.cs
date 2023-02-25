using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LungoApp.Views.LeftPanel
{
    /// <summary>
    /// Interaction logic for leftPanel.xaml
    /// </summary>
    public partial class LeftPanelView : UserControl
    {
        public LeftPanelView()
        {
            InitializeComponent();

            //var T = new TranslateTransform(40, 0);
            //Duration duration = new Duration(new TimeSpan(0, 0, 0, 1, 0));
            //DoubleAnimation anim = new DoubleAnimation(30, duration);
            //T.BeginAnimation(TranslateTransform.YProperty, anim);
        }

        private void menuAdd_Click(object sender, RoutedEventArgs e)
        {
        }

        private void menuTest_Click(object sender, RoutedEventArgs e)
        {
        }

        
    }
}
