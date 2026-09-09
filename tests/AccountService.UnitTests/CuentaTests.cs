using AccountService.Domain.Entities;
using AccountService.Domain.Exceptions;

namespace AccountService.UnitTests;

public class CuentaTests
{
    [Fact]
    public void AplicarMovimiento_DebeActualizarSaldo()
    {
        // Arrange
        var cuenta = new Cuenta(
            "1234567890",
            "Ahorros",
            100m,
            "CLI-001"
        );

        // Act
        cuenta.AplicarMovimiento(50m);

        // Assert
        Assert.Equal(150m, cuenta.SaldoDisponible);
    }

    [Fact]
    public void AplicarMovimiento_DebeLanzarExcepcion_SiSaldoEsInsuficiente()
    {
        // Arrange
        var cuenta = new Cuenta(
            "1234567890",
            "Ahorros",
            100m,
            "CLI-001"
        );

        // Act & Assert
        var exception = Assert.Throws<SaldoNoDisponibleException>(
            () => cuenta.AplicarMovimiento(-150m)
        );

        Assert.Equal("Saldo no disponible", exception.Message);
        Assert.Equal(100m, cuenta.SaldoDisponible);
    }
}