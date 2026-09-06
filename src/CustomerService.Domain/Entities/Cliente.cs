namespace CustomerService.Domain.Entities;

public class Cliente : Persona
{
    public string ClienteId { get; protected set; } = string.Empty;

    public string PasswordHash { get; protected set; } = string.Empty;

    public bool Estado { get; protected set; }

    private Cliente()
    {
    }

    public Cliente(
        string nombre,
        string genero,
        int edad,
        string identificacion,
        string direccion,
        string telefono,
        string clienteId,
        string passwordHash)
    {
        Id = Guid.NewGuid();

        Nombre = nombre;
        Genero = genero;
        Edad = edad;
        Identificacion = identificacion;
        Direccion = direccion;
        Telefono = telefono;

        ClienteId = clienteId;
        PasswordHash = passwordHash;
        Estado = true;
    }
}