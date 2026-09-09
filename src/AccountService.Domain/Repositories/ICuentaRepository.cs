using AccountService.Domain.Entities;

namespace AccountService.Domain.Repositories;

public interface ICuentaRepository
{
    Task<IEnumerable<Cuenta>> GetAllAsync();

    Task<Cuenta?> GetByIdAsync(Guid id);

    Task<Cuenta?> GetByNumeroCuentaAsync(
        string numeroCuenta);

    Task<IEnumerable<Cuenta>> GetByClienteIdAsync(
        string clienteId);

    Task AddAsync(Cuenta cuenta);

    Task UpdateAsync(Cuenta cuenta);
}