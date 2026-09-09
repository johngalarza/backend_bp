using AccountService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Infrastructure.Persistence;

public class AccountDbContext : DbContext
{
    public AccountDbContext(DbContextOptions<AccountDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cuenta> Cuentas => Set<Cuenta>();

    public DbSet<Movimiento> Movimientos => Set<Movimiento>();

    public DbSet<ClienteReadModel> ClientesReadModel => Set<ClienteReadModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.ToTable("cuentas");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.NumeroCuenta)
                .IsRequired()
                .HasMaxLength(20);

            entity.HasIndex(x => x.NumeroCuenta)
                .IsUnique();

            entity.Property(x => x.TipoCuenta)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(x => x.SaldoInicial)
                .HasPrecision(18, 2);

            entity.Property(x => x.SaldoDisponible)
                .HasPrecision(18, 2);

            entity.Property(x => x.Estado)
                .IsRequired();

            entity.Property(x => x.ClienteId)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasIndex(x => x.ClienteId);
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.ToTable("movimientos");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Fecha)
                .IsRequired();

            entity.Property(x => x.TipoMovimiento)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(x => x.Valor)
                .HasPrecision(18, 2);

            entity.Property(x => x.Saldo)
                .HasPrecision(18, 2);

            entity.HasOne<Cuenta>()
                .WithMany()
                .HasForeignKey(x => x.CuentaId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(x => new
            {
                x.CuentaId,
                x.Fecha
            });
        });

        modelBuilder.Entity<ClienteReadModel>(entity =>
        {
            entity.ToTable("clientes_read_model");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.ClienteId)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasIndex(x => x.ClienteId)
                .IsUnique();

            entity.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(x => x.Identificacion)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(x => x.Estado)
                .IsRequired();
        });
    }
}