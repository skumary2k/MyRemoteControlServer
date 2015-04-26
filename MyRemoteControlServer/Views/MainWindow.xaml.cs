using MyRemoteControlServer.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyRemoteControlServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MyNotifyIcon_TrayContextMenuOpen(object sender, System.Windows.RoutedEventArgs e)
        {
            //OpenEventCounter.Text = (int.Parse(OpenEventCounter.Text) + 1).ToString();
        }

        private void MyNotifyIcon_PreviewTrayContextMenuOpen(object sender, System.Windows.RoutedEventArgs e)
        {
            //marking the event as handled suppresses the context menu
            //e.Handled = (bool)SuppressContextMenu.IsChecked;

            //PreviewOpenEventCounter.Text = (int.Parse(PreviewOpenEventCounter.Text) + 1).ToString();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState != System.Windows.WindowState.Minimized)
                this.ShowInTaskbar = false;
            else
                this.Hide();
        }
    }
}
