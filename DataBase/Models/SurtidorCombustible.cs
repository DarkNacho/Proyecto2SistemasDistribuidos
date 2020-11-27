using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.Models
{
    public class SurtidorCombustible
    {
        [Key]
        public int Id { get; set; }
        public Surtidor Surtidor { get; set; }
        public Combustible Combustible { get; set; }
    }
}
