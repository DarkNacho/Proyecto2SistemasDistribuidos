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


        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = (Cliente.Combustibles[comboBox1.SelectedIndex].Precio * (1 + Cliente.utilidad)).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cliente.Send($"SF-{numericUpDown2.Value}-{numericUpDown1.Value}");
            //Thread.Sleep(100);
            Cliente.Combustibles[comboBox1.SelectedIndex].Precio = Cliente.Combustibles[comboBox1.SelectedIndex].NuevoPrecio;

        }
    }
}
