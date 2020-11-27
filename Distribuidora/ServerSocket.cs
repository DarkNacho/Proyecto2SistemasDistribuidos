using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace ServicioDistribuidora
{
    public class ServerSocket : TcpServer
    {
        public ServerSocket(IPAddress address, int port) : base(address, port) { }

        protected override TcpSession CreateSession() { return new SocketSession(this); }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"TCP server caught an error with code {error}");
        }
    }
}
