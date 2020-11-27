using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBase
{
    public class RepositoryDistribuidora : Repository<Distribuidora,int>
    {
        public RepositoryDistribuidora(ApplicationDbContext context) : base(context){}

        public new Distribuidora Get(int key) => Context.Distribuidoras.Where(x => x.Id == key).
            Include(b => b.Surtidores).FirstOrDefault();
        public new IEnumerable<Distribuidora> GetAll() => Context.Distribuidoras.Include(x => x.Surtidores);
        public IEnumerable<Combustible> GetAllCombustibleSurtidor(int idSurtidor) => Context.surtidorCombustibles.Include(b => b.Combustible).Where(a => a.Surtidor.Id == idSurtidor).Select(c => c.Combustible);

    }
}
