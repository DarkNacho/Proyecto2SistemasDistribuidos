using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.Models
{
    public class Distribuidora
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public float FactorUtilidad { get; set; }
        public List<Surtidor> Surtidores { get; set; } = new List<Surtidor>();
    }
}
