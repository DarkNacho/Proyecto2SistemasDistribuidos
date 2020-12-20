using ServicioSurtidor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Surtidor
{
    public partial class Form1 : Form
    {
        private static SocketClient Cliente;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
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
