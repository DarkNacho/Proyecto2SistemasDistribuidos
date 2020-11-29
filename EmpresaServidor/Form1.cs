using DataBase;
using DataBase.Models;
using ServicioEmpresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace EmpresaServidor
{
    public partial class Form1 : Form
    {
        List<Combustible> Combustibles;
        static public ServerSocket Server;
        public Form1()
        {
            InitializeComponent();
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
