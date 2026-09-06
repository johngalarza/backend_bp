using CustomerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Infrastructure.Persistence;

public class CustomerDbContext : DbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("clientes");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(x => x.Genero)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(x => x.Identificacion)
                .IsRequired()
                .HasMaxLength(20);

            entity.HasIndex(x => x.Identificacion)
                .IsUnique();

            entity.Property(x => x.Direccion)
                .IsRequired()
                .HasMaxLength(250);

            entity.Property(x => x.Telefono)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(x => x.ClienteId)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasIndex(x => x.ClienteId)
                .IsUnique();

            entity.Property(x => x.PasswordHash)
                .IsRequired();

            entity.Property(x => x.Estado)
                .IsRequired();
        });
    }
}