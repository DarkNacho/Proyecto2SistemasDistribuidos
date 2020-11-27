using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.Models
{
    public class Combustible
    {
        [Key]
        public string Tipo { get; set; }
        public int Precio { get; set; }
        public int NuevoPrecio { get; set; } //Para actualizaciones de precio.

        //cada vez que se quiera cargar combustible 
        //Precio = Precio != NuevoPrecio ? NuevoPrecio : Precio

    }
}
