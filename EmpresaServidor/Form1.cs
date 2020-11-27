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
        private UnitOfWork unitOfWork;
        List<Combustible> Combustibles;
        static public ServerSocket Server;
        public Form1()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            Combustibles = unitOfWork.Combustibles.GetAll().ToList();
            Combustibles.ForEach(x => comboBoxCombustible.Items.Add(x.Tipo));
        }

        private void comboBoxCombustible_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericPrecio.Value = Combustibles[comboBoxCombustible.SelectedIndex].Precio;
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
           
            var combustible = Combustibles[comboBoxCombustible.SelectedIndex];
            combustible.NuevoPrecio = Convert.ToInt32(numericPrecio.Value);
            unitOfWork.Combustibles.Update(combustible);
            unitOfWork.SaveChanges();
            
            //cambia en la DB y de paso avisa con multicast (?)

            //UP-Combustible-Precio
            Server.Multicast($"UP-{comboBoxCombustible.Text}-{numericPrecio.Value}");

        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            Server.Multicast("RP-0");
            //TODO: Generar Reporte 
            //ni idea como generarlo
        }
    }
}
