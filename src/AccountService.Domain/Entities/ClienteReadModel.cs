namespace AccountService.Domain.Entities;

public class ClienteReadModel
{
    public Guid Id { get; private set; }
    public string ClienteId { get; private set; } = string.Empty;
    public string Nombre { get; private set; } = string.Empty;
    public string Identificacion { get; private set; } = string.Empty;
    public bool Estado { get; private set; }

    private ClienteReadModel() { }

    public ClienteReadModel(
        Guid id,
        string clienteId,
        string nombre,
        string identificacion,
        bool estado)
    {
        Id = id;
        ClienteId = clienteId;
        Nombre = nombre;
        Identificacion = identificacion;
        Estado = estado;
    }

    public void Actualizar(
        string nombre,
        string identificacion,
        bool estado)
    {
        Nombre = nombre;
        Identificacion = identificacion;
        Estado = estado;
    }
}