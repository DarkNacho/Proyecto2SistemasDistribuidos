using DataBase;
using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EmpresaServidor
{
    public partial class Form1 : Form
    {
        private UnitOfWork unitOfWork;
        List<Combustible> Combustibles;
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
            var port = text[1];
            //TODO: IniciarServidor....
        }

        private void btnPrecio_Click(object sender, EventArgs e)
        {
            /*
            var combustible = Combustibles[comboBoxCombustible.SelectedIndex];
            combustible.NuevoPrecio = Convert.ToInt32(numericPrecio.Value);
            unitOfWork.Combustibles.Update(combustible);
            unitOfWork.SaveChanges();
            */
            var t = unitOfWork.Distribuidoras.GetAll().ToList();
            MessageBox.Show("t");

            //Solo actualiza NuevoPrecio, así se mantiene Precio por si alguien lo está utilizando.
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            //TODO: Generar Reporte
        }
    }
}
