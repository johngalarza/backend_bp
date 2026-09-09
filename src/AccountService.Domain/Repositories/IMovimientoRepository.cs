using AccountService.Domain.Entities;

namespace AccountService.Domain.Repositories;

public interface IMovimientoRepository
{
    Task<IEnumerable<Movimiento>> GetByCuentaIdAsync(
        Guid cuentaId);

    Task<IEnumerable<Movimiento>> GetByCuentaIdsAsync(
        IEnumerable<Guid> cuentaIds);

    Task AddAsync(Movimiento movimiento);
}