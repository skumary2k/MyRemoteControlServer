using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Windows;

namespace MyRemoteControlServer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            if (LogManager.GetCurrentLoggers().Length == 0)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory.ToString();
                string configFile = path + "log4net.config";
                XmlConfigurator.Configure(new FileInfo(configFile));
            } 

            var view = new MainWindow();
            view.DataContext = new ViewModels.MainViewModel();
            view.Show();
        }
    }
}
