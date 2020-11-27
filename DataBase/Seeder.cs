using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase
{
    //Dummy data fill...
    public static class Seeder
    {
        public static void Populate(ApplicationDbContext Context)
        {
            var c1 = new Combustible();
            c1.Precio = 93;
            c1.Tipo = "93";
            c1.NuevoPrecio = c1.Precio;

            var c2 = new Combustible();
            c2.Precio = 95;
            c2.Tipo = "95";
            c2.NuevoPrecio = c2.Precio;


            var c3 = new Combustible();
            c3.Precio = 97;
            c3.Tipo = "97";
            c3.NuevoPrecio = c3.Precio;


            var c4 = new Combustible();
            c4.Precio = 999;
            c4.Tipo = "Disel";
            c4.NuevoPrecio = c4.Precio;


            //surtidores
            var s1 = new Surtidor();
            var s2 = new Surtidor();


            //agregando combustible a sutridores
            var sc1 = new SurtidorCombustible();
            sc1.Combustible = c1;
            sc1.Surtidor = s1;
            var sc2 = new SurtidorCombustible();
            sc2.Surtidor = s1;
            sc2.Combustible = c4;
            //surtidor1 tiene combustible c1(93) y c4(disel)

            //
            var sc3 = new SurtidorCombustible();
            sc3.Combustible = c2;
            sc3.Surtidor = s2;
            var sc4 = new SurtidorCombustible();
            sc4.Combustible = c3;
            sc4.Surtidor = s2;
            //

            var d1 = new Distribuidora();
            d1.Nombre = "Distribuidora 1";
            d1.FactorUtilidad = 0.1f; // 10% 
            d1.Surtidores.Add(s1);
            d1.Surtidores.Add(s2);



            Context.Combustibles.Add(c1);
            Context.Combustibles.Add(c2);
            Context.Combustibles.Add(c3);
            Context.Combustibles.Add(c4);

            Context.Distribuidoras.Add(d1);

            Context.surtidorCombustibles.Add(sc1);
            Context.surtidorCombustibles.Add(sc2);
            Context.surtidorCombustibles.Add(sc3);
            Context.surtidorCombustibles.Add(sc4);

            Context.SaveChanges();
        }

        

     
    }
}
