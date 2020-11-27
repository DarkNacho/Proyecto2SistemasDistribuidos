using ServicioDistribuidora;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Distribuidora
{
    public partial class Form1 : Form
    {
        static public ServerSocket Server;
        static public SocketClient Client;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            var text = textBoxIpCliente.Text.Split(':');
            //TODO: Verificar formato de ip
            var ip = text[0];
            var port = Convert.ToInt32(text[1]);
            Client = new SocketClient(ip, port);
            Client.Connect();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            var text = textBoxIpServer.Text.Split(':');
            //TODO: Verificar formato de ip
            var ip = text[0];
            var port = Convert.ToInt32(text[1]);
            Server = new ServerSocket(IPAddress.Parse(ip), puerto);
            Server.Start();
        }
    }
}
