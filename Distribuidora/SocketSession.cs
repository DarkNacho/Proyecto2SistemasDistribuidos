using DataBase;
using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace ServicioDistribuidora
{
    class SocketSession : TcpSession
    {

        private int distribuidoraId;
        private UnitOfWork unitOfWork; 
        public SocketSession(TcpServer server, int id) : base(server) 
        {
            unitOfWork = UnitOfWork.GetInstance();
            distribuidoraId = id;
        }

        public SocketSession(TcpServer server) : base(server)
        {
        }

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
            unitOfWork = UnitOfWork.GetInstance();
            switch (request)
            {
                case "SF":
                    int id = Convert.ToInt32(message.Split('-')[1]);
                    int lt = Convert.ToInt32(message.Split('-')[2]);
                    var distribuidora = unitOfWork.Distribuidoras.Get(distribuidoraId);
                    var surtidor = distribuidora.Surtidores.Where(x => x.Id == id).FirstOrDefault();
                    surtidor.LitrosConsumidos += lt;
                    surtidor.CantidadCargas +=1;
                    unitOfWork.Surtidores.Update(surtidor);
                    unitOfWork.SaveChanges();
                    Server.Multicast("SF-OK");
                    break;
                case "CONECTION":
                    message = message.Substring(10);
                    string idCLiente = message.Split("::")[0];
                    int idSurtidor = Convert.ToInt32(message.Split("::")[1]);
                    //var tes = unitOfWork.Distribuidoras.GetAll().ToList();
                    var tes = unitOfWork.Distribuidoras.GetAllCombustibleSurtidor(idSurtidor).ToList();
                    var t = JsonSerializer.Serialize(tes);
                    ///var Client = Server.FindSession(new Guid(idCLiente));
                    Server.Multicast($"CONECTION-::::{t}::::{idCLiente}");
                    break;
                case "UTILIDAD":
                    Server.Multicast($"UTILIDAD-{unitOfWork.Distribuidoras[distribuidoraId].FactorUtilidad}");
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
