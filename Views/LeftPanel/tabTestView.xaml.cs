using LungoApp.Windows;
using LungoApp.Windows.Collections;
using LungoModel.Interfaces;
using LungoModel.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LungoApp.Views.LeftPanel
{
    /// <summary>
    /// Interaction logic for tabTest.xaml
    /// </summary>
    public partial class TabTestView : UserControl, ContextClosable
    {
        public TabTestView()
        {
            InitializeComponent();
            OpenContexts = new List<string>();
            Loaded += OnLoaded;
        }

        public List<string> OpenContexts { get; set; }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            TabTestViewModel vM = (TabTestViewModel)DataContext;
            vM.TestContextEvent += (context) => ShowContext(context);
            vM.ExitSessionEvent += (vM_) => PromptWarning(vM_);
        }

        private void PromptWarning(TabTestViewModel v)
        {
            PromptTestExitWarningWindow prompt = new PromptTestExitWarningWindow(v);
            prompt.Show();
        }

        private void ShowContext(StorageContext context)
        {
            ShowContextsWindow window = new ShowContextsWindow(context);
            window.parentWindow = this;
            OpenContexts.Add(context.Word);
            window.Show();
        }
    }
}
