namespace AccountService.Domain.Entities;

public class Movimiento
{
    public Guid Id { get; private set; }

    public DateTime Fecha { get; private set; }

    public string TipoMovimiento { get; private set; } = string.Empty;

    public decimal Valor { get; private set; }

    public decimal Saldo { get; private set; }

    public Guid CuentaId { get; private set; }

    private Movimiento()
    {
    }

    public Movimiento(
        string tipoMovimiento,
        decimal valor,
        decimal saldo,
        Guid cuentaId)
    {
        Id = Guid.NewGuid();
        Fecha = DateTime.UtcNow;
        TipoMovimiento = tipoMovimiento;
        Valor = valor;
        Saldo = saldo;
        CuentaId = cuentaId;
    }
}