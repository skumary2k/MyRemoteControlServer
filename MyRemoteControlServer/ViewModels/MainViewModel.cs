using log4net;
using MyRemoteControlServer.Commands;
using System.Windows;
using System.Windows.Input;

namespace MyRemoteControlServer.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private bool isChecked = true;
        private DelegateCommand exitCommand;
        private DelegateCommand serverCommand;
        private DelegateCommand startCommand;
        private DelegateCommand stopCommand;
        private string statusMsg;
        private static ILog logger = LogManager.GetLogger(typeof(MainViewModel));

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }

        public string StatusMessage
        {
            get { return statusMsg; }
            set
            {
                statusMsg = value;
                OnPropertyChanged("StatusMessage");
            }
        }

        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new DelegateCommand(Exit);
                }
                return exitCommand;
            }
        }

        public ICommand StartCommand
        {
            get
            {
                if (startCommand == null)
                {
                    startCommand = new DelegateCommand(StartServer);
                }
                return startCommand;
            }
        }

        public ICommand StopCommand
        {
            get
            {
                if (stopCommand == null)
                {
                    stopCommand = new DelegateCommand(StopServer);
                }
                return stopCommand;
            }
        }

        public ICommand ServerCommand
        {
            get
            {
                if (serverCommand == null)
                {
                    serverCommand = new DelegateCommand(StartOrStopServer);
                }
                return serverCommand;
            }
        }

        public MainViewModel()
        {
            this.statusMsg = ApplcationConstants.SERVER_NOT_STARTED;
        }
        private void StartOrStopServer()
        {

        }

        private void StartServer()
        {
            logger.Info("Starting server.");
        }


        private void StopServer()
        {
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
