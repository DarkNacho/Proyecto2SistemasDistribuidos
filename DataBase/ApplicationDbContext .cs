using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase
{
    public class ApplicationDbContext : DbContext
    {
      
        public DbSet<Combustible> Combustibles { get; set; }
        public DbSet<Surtidor> Surtidores { get; set; }
        public DbSet<Distribuidora> Distribuidoras{ get; set; }
        public DbSet<SurtidorCombustible> surtidorCombustibles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    }
}
