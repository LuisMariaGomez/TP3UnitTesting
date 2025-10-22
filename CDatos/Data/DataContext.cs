using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Entities;

namespace CDatos.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }
        public DataContext() { }
        public DbSet<Zombie> Zombie { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-SMHBKIH\\SQLEXPRESS;Initial Catalog=ZombieDB;Integrated Security=True;TrustServerCertificate=true;");
        }
    }
}
