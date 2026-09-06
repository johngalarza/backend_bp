using CustomerService.Domain.Entities;

namespace CustomerService.Domain.Repositories;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> GetAllAsync();

    Task<Cliente?> GetByIdAsync(Guid id);

    Task<Cliente?> GetByClienteIdAsync(string clienteId);

    Task<Cliente?> GetByIdentificacionAsync(string identificacion);

    Task AddAsync(Cliente cliente);

    Task UpdateAsync(Cliente cliente);

    Task DeleteAsync(Cliente cliente);
}