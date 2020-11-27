using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ServicioDistribuidora
{
    class SocketSession : TcpSession
    {

        public SocketSession(TcpServer server) : base(server) { }

        protected override void OnConnected()
        {
            Console.WriteLine($"TCP session with Id {Id} connected!");

            // Send invite message
            //string message = "Hello from TCP chat! Please send a message or '!' to disconnect the client!";
            //SendAsync(message);
        }

        protected override void OnDisconnected()
        {
            Console.WriteLine($"TCP session with Id {Id} disconnected!");
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            Console.WriteLine("Incoming: " + message);

            String request = message.Split('-')[0];
            switch (request)
            {
                case "SF":
                    String fuel = message.Split('-')[1];
                    String lt = message.Split('-')[2];
                    //Actualizar litros consumidos y cargas al combustible en tabla combustibles
                    break;
                default:
                    Console.WriteLine("Mensaje no identificado");
                    break;
            }
            
            // Multicast message to all connected sessions
            //Server.Multicast(message);

            // If the buffer starts with '!' the disconnect the current session
            //if (message == "!")
            //    Disconnect();
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"TCP session caught an error with code {error}");
        }

    }
}
