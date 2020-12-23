using DataBase;
using ServicioDistribuidora;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Distribuidora
{
    public partial class Form1 : Form
    {
        static public ServerSocket Server;
        static public SocketClient Client;
        public UnitOfWork unitOfWork;
        private ConfigurationModel Conf;
        public Form1()
        {
            InitializeComponent();
            
            Conf = JsonSerializer.Deserialize<ConfigurationModel>(File.ReadAllText("configuration.json"));
            textBoxIpCliente.Text = Conf.ClientServer.ToString();
            textBoxIpServer.Text = Conf.ServerInfo.ToString();
            UnitOfWork.DataBaseSource = Conf.DataBaseSource; //defaul normal
            if (!File.Exists(Conf.DataBaseSource))
            {
                UnitOfWork.DataBaseSource = Conf.BackUpDataBaseSource;
                MessageBox.Show("No se encuentra la base de dato, intentado respaldo");
                if (!File.Exists(Conf.BackUpDataBaseSource))
                {
                    MessageBox.Show("No se encuentra la base de dato de respaldo");
                    Environment.Exit(0);//No se puede trabajar sin base de datos...
                }
            }
            unitOfWork = UnitOfWork.GetInstance();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            //var text = textBoxIpCliente.Text.Split(':');
            //TODO: Verificar formato de ip
            //var ip = text[0];
            //var port = Convert.ToInt32(text[1]);
            //Client = new SocketClient(ip, port, Convert.ToInt32(numericUpDown2.Value), Server);
            //Client = new SocketClient(Conf.ClientConection.Ip, Conf.ClientConection.Port, Convert.ToInt32(numericUpDown2.Value), Server);
            //Client.Connect();
            var conected = ClientConection(Conf.ClientServer);
            if(!conected)
            {
                MessageBox.Show("No se pudo conectar, intentando servidor de respaldo");
                conected = ClientConection(Conf.BackUpClientServer);
            }
            if (conected)
            {
                MessageBox.Show("Conectado");
                btnConectar.Enabled = false;
                textBoxIpCliente.Enabled = false;
                Task.Run(WatchDog);
            }
            else MessageBox.Show("No se pudo conectar");
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            //var text = textBoxIpServer.Text.Split(':');
            //TODO: Verificar formato de ip
            //var ip = text[0];
            //var port = Convert.ToInt32(text[1]);
            //Server = new ServerSocket(IPAddress.Parse(ip), port, Convert.ToInt32(numericUpDown2.Value));
            Server = new ServerSocket(IPAddress.Parse(Conf.ServerInfo.Ip), Conf.ServerInfo.Port, Convert.ToInt32(numericUpDown2.Value));
            Server.Start();
            numericUpDown1.Value = (int)(unitOfWork.Distribuidoras[Convert.ToInt32(numericUpDown2.Value)].FactorUtilidad * 100);
            btnIniciar.Enabled = false;
            textBoxIpServer.Enabled = false;
            numericUpDown2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var utilidad = Convert.ToInt32(numericUpDown1.Value) / 100.0f;
            unitOfWork.Distribuidoras[Convert.ToInt32(numericUpDown2.Value)].FactorUtilidad = utilidad;
            unitOfWork.SaveChanges();
            Server.Multicast($"UTILIDAD-{utilidad}");
        }

        private void WatchDog()
        {
            while(true)
            {
                if (!Client.IsConnected)
                {
                    MessageBox.Show("Se ha desconectado sin querer, reconectando...");
                    var conected = ClientConection(Conf.ClientServer);
                    if(!conected)
                    {
                        MessageBox.Show("No se pudo renectar, intentando en servidor de respaldo");
                        conected = ClientConection(Conf.BackUpClientServer);
                        if (!conected) MessageBox.Show("Ambos fallaron... ahora debería hacer algo");
                        else MessageBox.Show("Conectado en respaldo");
                    }
                }
            }
        }

        private bool ClientConection(ServerInfoModel info)
        {
            Client = new SocketClient(info.Ip, info.Port, Convert.ToInt32(numericUpDown2.Value), Server);
            DateTime startTime = DateTime.Now;
            Client.ConnectAsync();
            while (!Client.IsConnected)
            {
                if (DateTime.Now.Subtract(startTime).TotalMilliseconds > 5000)
                {
                    Client.DisconnectAndStop();
                    return false;
                }
            }
            return true;
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Client.IsConnected.ToString());
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
