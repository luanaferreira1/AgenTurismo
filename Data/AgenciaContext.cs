using AgenTurismo.Models;
using Microsoft.EntityFrameworkCore;

namespace AgenTurismo.Data
{
    public class AgenciaContext : DbContext
    {
        public AgenciaContext(DbContextOptions<AgenciaContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Destino> Destinos { get; set; }
        public DbSet<PacoteTuristico> PacotesTuristicos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PacoteTuristico>()
                .HasMany(p => p.Destinos)
                .WithMany(d => d.PacotesTuristicos)
                .UsingEntity(j => j.ToTable("PacoteDestinos"));

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Cliente)
                .WithMany(c => c.Reservas)
                .HasForeignKey(r => r.ClienteId)
                .IsRequired(false) 
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.PacoteTuristico)
                .WithMany(p => p.Reservas)
                .HasForeignKey(r => r.PacoteTuristicoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PacoteTuristico>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Cliente>()
                .HasQueryFilter(c => !c.IsDeleted);

            modelBuilder.Entity<Reserva>()
                .HasQueryFilter(r => r.Cliente == null || !r.Cliente.IsDeleted);

            modelBuilder.Entity<PacoteTuristico>()
                .Property(p => p.DataInicio)
                .HasDefaultValue(DateTime.Now.AddDays(7));

            modelBuilder.Entity<PacoteTuristico>()
                .Property(p => p.CapacidadeMaxima)
                .HasDefaultValue(10);

            modelBuilder.Entity<PacoteTuristico>()
                .Property(p => p.Preco)
                .HasDefaultValue(1000m);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=AgenTurismo.db",
                    options => options.CommandTimeout(30));
            }
        }
    }
}