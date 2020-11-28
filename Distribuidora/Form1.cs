using DataBase;
using ServicioDistribuidora;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Distribuidora
{
    public partial class Form1 : Form
    {
        static public ServerSocket Server;
        static public SocketClient Client;
        public UnitOfWork unitOfWork;
        public Form1()
        {
            InitializeComponent();
            unitOfWork = UnitOfWork.GetInstance();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            var text = textBoxIpCliente.Text.Split(':');
            //TODO: Verificar formato de ip
            var ip = text[0];
            var port = Convert.ToInt32(text[1]);
            Client = new SocketClient(ip, port, Convert.ToInt32(numericUpDown2.Value), Server);
            Client.Connect();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            var text = textBoxIpServer.Text.Split(':');
            //TODO: Verificar formato de ip
            var ip = text[0];
            var port = Convert.ToInt32(text[1]);
            Server = new ServerSocket(IPAddress.Parse(ip), port, Convert.ToInt32(numericUpDown2.Value));
            Server.Start();
            numericUpDown1.Value = (int)(unitOfWork.Distribuidoras[Convert.ToInt32(numericUpDown2.Value)].FactorUtilidad * 100);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var utilidad = Convert.ToInt32(numericUpDown1.Value) / 100.0f;
            unitOfWork.Distribuidoras[Convert.ToInt32(numericUpDown2.Value)].FactorUtilidad = utilidad;
            unitOfWork.SaveChanges();
            Server.Multicast($"UTILIDAD-{utilidad}");
        }
    }
}
