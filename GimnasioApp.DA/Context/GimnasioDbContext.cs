using GimnasioApp.Model;
using Microsoft.EntityFrameworkCore;

namespace GimnasioApp.DA.Context
{
    public class GimnasioDbContext : DbContext
    {
        public GimnasioDbContext(DbContextOptions<GimnasioDbContext> options) : base(options) { }

        public DbSet<Membresia> Membresias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Membresia>(entity =>
            {
                entity.ToTable("Membresias");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Identificacion).IsRequired().HasColumnType("varchar(max)");
                entity.Property(e => e.Nombre).IsRequired().HasColumnType("varchar(max)");
                entity.Property(e => e.Apellidos).IsRequired().HasColumnType("varchar(max)");
                entity.Property(e => e.Telefono).IsRequired().HasColumnType("varchar(max)");
                entity.Property(e => e.PrecioBaseMensual).HasColumnType("decimal(18,2)");
                entity.Property(e => e.MontoTotal).HasColumnType("decimal(18,2)");
                entity.Property(e => e.FechaDeInicio).HasColumnType("datetime");
                entity.Property(e => e.FechaDeVencimiento).HasColumnType("datetime");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
