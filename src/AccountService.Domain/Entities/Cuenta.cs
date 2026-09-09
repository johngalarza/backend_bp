using AccountService.Domain.Exceptions;

namespace AccountService.Domain.Entities;

public class Cuenta
{
    public Guid Id { get; private set; }

    public string NumeroCuenta { get; private set; } = string.Empty;

    public string TipoCuenta { get; private set; } = string.Empty;

    public decimal SaldoInicial { get; private set; }

    public decimal SaldoDisponible { get; private set; }

    public bool Estado { get; private set; }

    public string ClienteId { get; private set; } = string.Empty;

    private Cuenta()
    {
    }

    public Cuenta(
        string numeroCuenta,
        string tipoCuenta,
        decimal saldoInicial,
        string clienteId)
    {
        Id = Guid.NewGuid();

        NumeroCuenta = numeroCuenta;
        TipoCuenta = tipoCuenta;
        SaldoInicial = saldoInicial;
        SaldoDisponible = saldoInicial;
        Estado = true;
        ClienteId = clienteId;
    }

    public void Actualizar(
        string tipoCuenta,
        bool estado)
    {
        TipoCuenta = tipoCuenta;
        Estado = estado;
    }

    public void AplicarMovimiento(decimal monto)
    {
        var nuevoSaldo = SaldoDisponible + monto;

        if (nuevoSaldo<0)
            throw new SaldoNoDisponibleException();
            
        SaldoDisponible += monto;
    }
}