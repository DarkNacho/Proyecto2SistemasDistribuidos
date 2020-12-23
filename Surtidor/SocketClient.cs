using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataBase.Models;
using System.Text.Json;
using System.Linq;
using System.IO;

namespace ServicioSurtidor
{
    class SocketClient : TcpClient
    {
        private bool _stop;
        public List<DataBase.Models.Combustible> Combustibles;
        public int litrosVendidos = 0;
        public float utilidad = 0;
        private int distribuidoraId;
        public bool serverUp;

        public SocketClient(string address, int port, int id) : base(address, port) { distribuidoraId = id; }

        protected override void OnConnected()
        {
            Console.WriteLine($"TCP client connected a new session with Id {Id}");
            Task.Run(() => StartListening());
        }

        private void StartListening()
        {
            Console.WriteLine($"TCP client {Id} started listening to server");
            while (1 == 1)
            {
                byte[] buffer = new byte[4028];
                long bytes = Receive(buffer);
            }
        }

        protected override void OnDisconnected()
        {
            Console.WriteLine($"TCP client disconnected a session with Id {Id}");
         
            // Wait for a while...
            //Thread.Sleep(1000);

            // Try to connect again
            //if (!_stop)
            //    ConnectAsync();
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            Console.WriteLine(Encoding.UTF8.GetString(buffer, (int)offset, (int)size));
            String message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            String request = message.Split('-')[0];
            serverUp = false;
            switch (request)
            {
                //Server.Multicast($"CONECTION-::::{t}::::{idCLiente}");
                case "CONECTION":
                    var parse = message.Split("::::");
                    if (parse[2] != Id.ToString()) break; 
                    Combustibles = JsonSerializer.Deserialize<List<Combustible>>(parse[1]);
                   break;
                case "UTILIDAD":
                    utilidad = Convert.ToSingle(message.Split('-')[1]);
                    break;
                case "UP":
                    Send($"CONECTION-{Id}::{distribuidoraId}");
                    break;
                case "SF":
                    serverUp = true;
                    break;
            }
        }

        protected override void OnError(System.Net.Sockets.SocketError error)
        {
            Console.WriteLine($"TCP client caught an error with code {error}");
        }

        public void DisconnectAndStop()
        {
            _stop = true;
            DisconnectAsync();
            while (IsConnected)
                Thread.Yield();
        }
    }
}
