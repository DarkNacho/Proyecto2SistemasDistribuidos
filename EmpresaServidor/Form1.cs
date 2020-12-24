using DataBase;
using DataBase.Models;
using ServicioEmpresa;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Windows.Forms;

namespace EmpresaServidor
{
    public partial class Form1 : Form
    {
        List<Combustible> Combustibles;
        static public ServerSocket Server;
        ConfigurationModel Conf;
        public Form1()
        {
            InitializeComponent();

            Conf = JsonSerializer.Deserialize<ConfigurationModel>(File.ReadAllText("EmpresaConf.json"));
            comboBoxCombustible.Items.AddRange(Conf.Combustibles);
            if (Conf.Autoconnection) btnConectar_Click(this, null);

        }

        private void comboBoxCombustible_SelectedIndexChanged(object sender, EventArgs e)
        {
            //numericPrecio.Value = Combustibles[comboBoxCombustible.SelectedIndex].Precio;
            //quizas debería dejar NuevoPrecio
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            var text = textBoxIp.Text.Split(':');
            //TODO: Verificar formato de ip
            var ip = text[0];
            var port = Convert.ToInt32(text[1]);
            Server = new ServerSocket(IPAddress.Parse(ip), port);
            Server.Start();
            btnConectar.Enabled = false;
            textBoxIp.Enabled = false;
        }

        private void btnPrecio_Click(object sender, EventArgs e)
        {
            Server.Multicast($"UP-{comboBoxCombustible.Text}-{numericPrecio.Value}");
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            Server.Multicast("RP-0");
            //TODO: Generar Reporte 
            //ni idea como generarlo
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
