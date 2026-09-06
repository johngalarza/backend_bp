namespace CustomerService.Domain.Entities;

public abstract class Persona
{
    public Guid Id { get; protected set; }

    public string Nombre { get; protected set; } = string.Empty;

    public string Genero { get; protected set; } = string.Empty;

    public int Edad { get; protected set; }

    public string Identificacion { get; protected set; } = string.Empty;

    public string Direccion { get; protected set; } = string.Empty;

    public string Telefono { get; protected set; } = string.Empty;
}