using DataBase;
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
        int id;
        public ServerSocket(IPAddress address, int port, int _id) : base(address, port) { id = _id; }

        protected override TcpSession CreateSession() { return new SocketSession(this, id); }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"TCP server caught an error with code {error}");
        }
    }
}
