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
        UnitOfWork unitOfWork;
        public ServerSocket(IPAddress address, int port, int _id, UnitOfWork context) : base(address, port) { id = _id;  unitOfWork = context; }

        protected override TcpSession CreateSession() { return new SocketSession(this, id, unitOfWork); }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"TCP server caught an error with code {error}");
        }
    }
}
