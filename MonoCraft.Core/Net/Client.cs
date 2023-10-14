using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Core.Net
{
    public abstract class Client : IClient
    {

        private Socket _socket;
        private NetworkStream _networkStream;

        private string _address;
        private ushort _port;

        public event Action ConnectionEstablished;

        public bool IsConnected => _socket.Connected;
        public int Available => _socket.Available;
        public string Address => _address;
        public ushort Port => _port;

        protected Client()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void OnConnectionEstablished()
        {
            _networkStream = new NetworkStream(_socket, true);
            ConnectionEstablished?.Invoke();
        }

        public virtual void Connect(string address, ushort port)
        {
            _address = address;
            _port = port;
            try
            {
                _socket.Connect(_address, _port);
                OnConnectionEstablished();
            }
            catch (Exception ex)
            {
                _socket.Close();
                Console.WriteLine(ex.ToString());
            }
        }

        public virtual async Task ConnectAsync(string address, ushort port)
        {
            _address = address;
            _port = port;
            try
            {
                await _socket.ConnectAsync(_address, _port);
                OnConnectionEstablished();
            }
            catch (Exception ex)
            {
                _socket.Close();
                Console.WriteLine(ex.ToString());
            }
        }

        public virtual void Disconnect(bool reuse = false)
        {
            try
            {
                _socket.Disconnect(reuse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        
        public virtual async Task DisconnectAsync(bool reuse = false)
        {
            try
            {
                await _socket.DisconnectAsync(reuse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void ReConnect()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Connect(Address, Port);
        }
        
        public virtual void Close()
        {
            try
            {
                _socket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public NetworkStream GetStream() { return _networkStream; }

    }
}
