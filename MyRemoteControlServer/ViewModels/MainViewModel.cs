using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using log4net;
using MyRemoteControlServer.Commands;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
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
        private Thread conn = null;
        private Guid guid = new Guid("{E075D486-E23D-4887-8AF5-DAA1F6A5B172}");
        private BluetoothListener blueToothListener;
        private bool listeningOnBluetooth = true;

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
            this.statusMsg = ApplicationConstants.SERVER_NOT_STARTED;
        }
        private void StartOrStopServer()
        {
            if (!this.IsChecked) StartServer();
            else StopServer();
        }

        private void StartServer()
        {
            logger.Info("Starting my server.");
            this.conn = new Thread(new ThreadStart(this.StartBluetoothService));
            this.conn.Start();
        }

        private void StartBluetoothService()
        {
            logger.Debug("StartBluetoothService");
            try
            {
                var bluetoothDeviceInfo = new BluetoothDeviceInfo(BluetoothRadio.PrimaryRadio.LocalAddress);
                logger.Debug(BluetoothService.SerialPort);
                this.StatusMessage = ApplicationConstants.SERVER_LISTENING;
                
                this.blueToothListener = new BluetoothListener(this.guid);
                this.blueToothListener.Start();
                
                while (this.listeningOnBluetooth)
                {
                    BluetoothClient bluetoothClient = null;
                    this.StatusMessage = ApplicationConstants.SERVER_LISTENING;
                    bluetoothClient = this.blueToothListener.AcceptBluetoothClient();
                    
                    var streamReader = new StreamReader(bluetoothClient.GetStream());
                    var streamWriter = new StreamWriter(bluetoothClient.GetStream());
                    streamWriter.WriteLine("connected");
                    streamWriter.Flush();
                    logger.Info("Connected to remote bluetooth device.");
                    this.StatusMessage = ApplicationConstants.SERVER_CONNECTED;

                    while (!streamReader.EndOfStream)
                    {
                        if (streamReader.Peek() >= 0)
                        {
                            string streamData = streamReader.ReadLine();
                            logger.Debug(streamData);
                            SendKeys.SendWait("{" + streamData.Trim().ToUpper() + "}");
                            streamWriter.Flush();
                        }
                    }
                    streamReader.Close();
                    streamWriter.Close();
                    this.StatusMessage = ApplicationConstants.SERVER_LISTENING;
                }
            }
            catch (Exception exception)
            {
                logger.Error("Exception occured: " + exception.Message);
            }
        }

        private void StopServer()
        {
            try
            {
                if (this.conn != null)
                {
                    if (this.blueToothListener != null)
                        this.blueToothListener.Stop();
                    this.conn.Abort();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error while stopping the server: " + ex.Message);
            }
        }

        private void Exit()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}