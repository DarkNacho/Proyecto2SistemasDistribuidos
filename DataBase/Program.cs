using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;

namespace DataBase
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            options.UseSqlite("Data Source=test.db");
            ApplicationDbContext dbContext = new ApplicationDbContext(options.Options);
            dbContext.Database.EnsureCreated();

            
            var c1 = new Combustible();
            c1.Precio = 93;
            c1.Tipo = "93";

            var c2 = new Combustible();
            c2.Precio = 95;
            c2.Tipo = "95";

            var c3 = new Combustible();
            c3.Precio = 97;
            c3.Tipo = "97";

            var c4 = new Combustible();
            c4.Precio = 999;
            c4.Tipo = "Disel";


            dbContext.Combustibles.Add(c1);
            dbContext.Combustibles.Add(c2);
            dbContext.Combustibles.Add(c3);
            dbContext.Combustibles.Add(c4);
            dbContext.SaveChanges();
            
        }
    }
}
