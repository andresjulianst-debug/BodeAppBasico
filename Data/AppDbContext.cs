using BodeAppBasico.Models;
using Microsoft.EntityFrameworkCore;

namespace BodeAppBasico.Data
{
    public class AppDbContext : DbContext

    // Esta es el componente del framework que se encarga de conectar la aplicación con la base de datos
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Estas son tus tablas reales en la base de datos
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Bodega> Bodegas { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Factura> Facturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Llaves primarias
            modelBuilder.Entity<Cliente>().HasKey(c => c.Cedula);
            modelBuilder.Entity<Producto>().HasKey(p => p.Codigo);
            modelBuilder.Entity<Bodega>().HasKey(b => b.Id);
            modelBuilder.Entity<Inventario>().HasKey(i => i.Id);
            modelBuilder.Entity<Factura>().HasKey(f => f.Numero);
            modelBuilder.Entity<DetalleFactura>().HasKey(d => d.Id);

            // Poblar las bodegas automáticamente al crear la base de datos
            modelBuilder.Entity<Bodega>().HasData(
                new Bodega { Id = 1, Nombre = "1 - Rionegro (Central)", EsCentral = true },
                new Bodega { Id = 2, Nombre = "2 - El Peñol", EsCentral = false },
                new Bodega { Id = 3, Nombre = "3 - Marinilla", EsCentral = false },
                new Bodega { Id = 4, Nombre = "4 - La Ceja", EsCentral = false },
                new Bodega { Id = 5, Nombre = "5 - Guarne", EsCentral = false }
            
            );
        }
    }
}
