﻿using DataBase;
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
        public bool _stop;
        private int distribuidraId;
        private ServerSocket Server;
    
        public SocketClient(string address, int port, int id, ServerSocket server) : base(address, port)
        {
            unitOfWork = UnitOfWork.GetInstance();
            distribuidraId = id;
            Server = server;
          
        }
        private UnitOfWork unitOfWork;

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
                byte[] buffer = new byte[2048];
                long bytes = Receive(buffer);
            }
        }

        protected override void OnDisconnected()
        {
            Console.WriteLine($"TCP client disconnected a session with Id {Id}");

            // Wait for a while...
            //Thread.Sleep(1000);

            // Try to connect again
            //if (!_stop) ConnectAsync();
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            Console.WriteLine(Encoding.UTF8.GetString(buffer, (int)offset, (int)size));
            String message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            String request = message.Split('-')[0];
            unitOfWork = UnitOfWork.GetInstance();
            switch (request)
            {
                case "UP":
                    String fuel = message.Split('-')[1];
                    int price = Convert.ToInt32(message.Split('-')[2]);

                    var combustible = unitOfWork.Combustibles[fuel];
                    combustible.NuevoPrecio = price;
                    combustible.Precio = price;
                    unitOfWork.Combustibles.Update(combustible);
                    unitOfWork.SaveChanges();
                    //Cambiar precio del combustible indicado en tabla combustibles (recordar usar factor de utilidad).
                    //informar a los clientes
                    Server.Multicast("UP-bla");
                    break;
                case "RP":
                    
                    var surtidores = unitOfWork.Distribuidoras.Get(distribuidraId).Surtidores;
                    var info = $"Cliente: {Id}\n";
                    foreach (var surtidor in surtidores)
                        info += $"Suritdo: {surtidor.Id} Ha consumido: {surtidor.LitrosConsumidos} y se ha cargado {surtidor.CantidadCargas}\n";
                    //Enviar reporte; String con datos de cada distribuidora.
                    //String reporte = $"RP-{info}";
                    Send(info);
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
