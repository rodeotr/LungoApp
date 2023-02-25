using LungoViewModels.ViewModels.Test;
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
    /// Interaction logic for warningPrompt.xaml
    /// </summary>
    public partial class PromptTestExitWarningWindow : Window
    {
        TabTestViewModel _vM;
        public PromptTestExitWarningWindow(TabTestViewModel vM)
        {
            _vM = vM;
            InitializeComponent();
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void ExitSession(object sender, RoutedEventArgs e)
        {
            _vM.ExitSession();
            Close();
        }
    }
}
