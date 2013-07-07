using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace WP8_SampleBluetooth.Bluetooth
{

    public delegate void Received(object Data, StreamSocket socket, IChannel channel, DateTime Timestamp);

    public class Connection
    {
        public event Received OnReceived;

        public Connection(string peerName, bool autoConnect = false)
        {
            _peerName = peerName;
            if (autoConnect) Open();
        }
        public void Open()
        {
            _Open();
        }
        private async void _Open()
        {
            await SetupDeviceConn();
        }
        private StreamSocket _socket = null;
        private DataWriter _dataWriter = null;
        private DataReader _dataReader = null;
        private string _peerName = "AGENT";

        private async void ReadData()
        {
            while (true)
            {
                await _dataReader.LoadAsync(255);
                string data = string.Empty;

                while (_dataReader.UnconsumedBufferLength > 0)
                {
                    data += _dataReader.ReadString(_dataReader.UnconsumedBufferLength);
                }

                if (OnReceived != null) OnReceived(data, _socket, null, DateTime.Now);

            }
        }

        public bool IsOpen { get { return _connected; } }
        private bool _connected = false;
        
        public async void SendData(string payload)
        {
            if (_connected)
            {
                _dataWriter.WriteString(payload + '\0');
                await _dataWriter.StoreAsync();
            }

        }


        private async Task<bool> SetupDeviceConn()
        {
            //Connect to your paired host PC using BT + StreamSocket (over RFCOMM)
            PeerFinder.AlternateIdentities["Bluetooth:PAIRED"] = "";

            var devices = await PeerFinder.FindAllPeersAsync();

            if (devices.Count == 0)
            {
                MessageBox.Show("No paired device");
                await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-bluetooth:"));
                return false;
            }

            var peerInfo = devices.FirstOrDefault(c => c.DisplayName.Contains(_peerName));
            if (peerInfo == null)
            {
                MessageBox.Show("No paired device");
                await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-bluetooth:"));
                return false;
            }

            _socket = new StreamSocket();

            //"{00001101-0000-1000-8000-00805f9b34fb}" - is the GUID for the serial port service.
            await _socket.ConnectAsync(peerInfo.HostName, "{00001101-0000-1000-8000-00805f9b34fb}");

            _dataWriter = new DataWriter(_socket.OutputStream);
            _dataReader = new DataReader(_socket.InputStream);
            _dataReader.InputStreamOptions = InputStreamOptions.Partial;

            ReadData();

            _connected = true;
            return true;
        }


    }
}
