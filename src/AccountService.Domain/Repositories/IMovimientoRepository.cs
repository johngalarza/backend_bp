using AccountService.Domain.Entities;

namespace AccountService.Domain.Repositories;

public interface IMovimientoRepository
{
    Task<IEnumerable<Movimiento>> GetByCuentaIdAsync(Guid cuentaId);

    Task AddAsync(Movimiento movimiento);
}