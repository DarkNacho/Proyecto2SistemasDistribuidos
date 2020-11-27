using DataBase;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DataBase
{
    public class UnitOfWork : IDisposable
    {
        public readonly ApplicationDbContext _context;
        public Repository<Combustible, string> Combustibles{ get; set; }
        public RepositoryDistribuidora Distribuidoras{ get; set; }
        public UnitOfWork()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            options.UseSqlite("Data Source=test.db");
            ApplicationDbContext dbContext = new ApplicationDbContext(options.Options);
            //dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            //Seeder.Populate(dbContext);
            
            _context = dbContext;

            Combustibles = new Repository<Combustible,string>(_context);
            Distribuidoras = new RepositoryDistribuidora(_context);
        }
        public int SaveChanges() => _context.SaveChanges();
        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
