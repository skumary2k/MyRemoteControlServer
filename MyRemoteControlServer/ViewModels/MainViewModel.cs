using MyRemoteControlServer.Commands;
using System.Windows;
using System.Windows.Input;

namespace MyRemoteControlServer.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private bool isChecked;
        private DelegateCommand exitCommand;
        private DelegateCommand serverCommand;

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged("IsChecked");
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

        private void StartOrStopServer()
        {

        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
