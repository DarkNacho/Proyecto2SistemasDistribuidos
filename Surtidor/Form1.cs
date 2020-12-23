using ServicioSurtidor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Surtidor
{
    public partial class Form1 : Form
    {
        private static SocketClient Cliente;
        ConfigurationModel Conf;

        public Form1()
        {
            InitializeComponent();
            Conf = JsonSerializer.Deserialize<ConfigurationModel>(File.ReadAllText("configuration.json"));
            textBoxIp.Text = Conf.ClientServer.ToString();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            /*
            var text = textBoxIp.Text.Split(':');
            //TODO: Verificar formato de ip
            var ip = text[0];
            var port = Convert.ToInt32(text[1]);
            Cliente = new SocketClient(ip, port, Convert.ToInt32(numericUpDown2.Value));
            Cliente.Connect();
            Cliente.Send($"CONECTION-{Cliente.Id}::{numericUpDown2.Value}");
            //MessageBox.Show(Cliente.Id.ToString());
            while (true)
            {
                if (Cliente.Combustibles != null)
                {
                    Cliente.Combustibles.ForEach(x => comboBox1.Items.Add(x.Tipo));
                    break;
                }
            }
            Cliente.Send($"UTILIDAD-bla");
            btnConectar.Enabled = false;
            comboBox1.SelectedItem = "93";
            textBoxIp.Enabled = false;
            numericUpDown2.Enabled = false;
            */
            var conected = ClientConection(Conf.ClientServer);
            if (!conected)
            {
                MessageBox.Show("No se pudo conectar, intentando servidor de respaldo");
                conected = ClientConection(Conf.BackUpClientServer);
            }
            if (conected)
            {
                MessageBox.Show("Conectado");
                Cliente.Send($"CONECTION-{Cliente.Id}::{numericUpDown2.Value}");
                while (true)
                {
                    if (Cliente.Combustibles != null)
                    {
                        Cliente.Combustibles.ForEach(x => comboBox1.Items.Add(x.Tipo));
                        break;
                    }
                }
                Cliente.Send($"UTILIDAD-bla");
                btnConectar.Enabled = false;
                comboBox1.SelectedItem = "93";
                textBoxIp.Enabled = false;
                numericUpDown2.Enabled = false;
                Task.Run(WatchDog);
            }
            else MessageBox.Show("No se pudo conectar");
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = (Cliente.Combustibles[comboBox1.SelectedIndex].Precio * (1 + Cliente.utilidad)).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cliente.Send($"SF-{numericUpDown2.Value}-{numericUpDown1.Value}");
            Thread.Sleep(5000);
            if (!Cliente.serverUp)
                Cliente.litrosVendidos += Convert.ToInt32(numericUpDown1.Value);
            else if (Cliente.serverUp && Cliente.litrosVendidos > 0) {
                Cliente.Send($"SF-{numericUpDown2.Value}-{Cliente.litrosVendidos}");
                Cliente.litrosVendidos = 0;
            }
            Cliente.Combustibles[comboBox1.SelectedIndex].Precio = Cliente.Combustibles[comboBox1.SelectedIndex].NuevoPrecio;
        }

        private void WatchDog()
        {
            while (true)
            {
                if (!Cliente.IsConnected)
                {
                    MessageBox.Show("Se ha desconectado sin querer, reconectando...");
                    var conected = ClientConection(Conf.ClientServer);
                    if (!conected)
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
            Cliente = new SocketClient(info.Ip, info.Port, Convert.ToInt32(numericUpDown2.Value));
            DateTime startTime = DateTime.Now;
            Cliente.ConnectAsync();
            while (!Cliente.IsConnected)
            {
                if (DateTime.Now.Subtract(startTime).TotalMilliseconds > 5000)
                {
                    Cliente.DisconnectAndStop();
                    return false;
                }
            }
            return true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
