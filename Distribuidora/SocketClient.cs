using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicioDistribuidora
{
    public class SocketClient : TcpClient
    {
        private bool _stop;

        public SocketClient(string address, int port) : base(address, port) { }

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
                byte[] buffer = new byte[1024];
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
            switch (request)
            {
                case "UP":
                    String fuel = message.Split('-')[1];
                    String price = message.Split('-')[2];
                    //Cambiar precio del combustible indicado en tabla combustibles (recordar usar factor de utilidad).
                    break;
                case "RP":
                    //Enviar reporte; String con datos de cada distribuidora.
                    String reporte = "RP-reporte";
                    Send(reporte);
                    break;
                default:
                    Console.WriteLine("Mensaje no identificado");
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
