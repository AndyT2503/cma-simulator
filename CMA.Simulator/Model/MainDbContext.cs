using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMA_Simulator.Model
{
    public class MainDbContext : DbContext
    {
        public DbSet<CargoEquipment> CargoEquipments { get; set; }
        public DbSet<Container> Containers { get; set; }
        public DbSet<Shipment> Shipments { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresExtension("pgcrypto")
                .HasPostgresExtension("uuid-ossp");

            builder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly);
        }

    }
}
