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
        decimal valor,
        decimal saldo,
        Guid cuentaId)
    {
        if (valor == 0)
            throw new ArgumentException(
                "El valor del movimiento no puede ser cero.");

        Id = Guid.NewGuid();

        Fecha = DateTime.UtcNow;

        Valor = valor;

        TipoMovimiento = valor > 0
            ? "Deposito"
            : "Retiro";

        Saldo = saldo;

        CuentaId = cuentaId;
    }
}