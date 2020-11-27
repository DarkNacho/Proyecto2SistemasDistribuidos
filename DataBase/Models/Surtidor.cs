using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.Models
{
    public class Surtidor
    {
        [Key]
        public int Id { get; set; }
        public int LitrosConsumidos { get; set; }
        public int CantidadCargas { get; set; }
        public List<SurtidorCombustible> SurtidorCombustible { get; set; } = new List<SurtidorCombustible>();

    }
}
