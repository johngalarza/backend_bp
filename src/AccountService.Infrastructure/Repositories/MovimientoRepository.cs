using AccountService.Domain.Entities;
using AccountService.Domain.Repositories;
using AccountService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Infrastructure.Repositories;

public class MovimientoRepository : IMovimientoRepository
{
    private readonly AccountDbContext _context;

    public MovimientoRepository(AccountDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Movimiento>> GetByCuentaIdAsync(
        Guid cuentaId)
    {
        return await _context.Movimientos
            .AsNoTracking()
            .Where(m => m.CuentaId == cuentaId)
            .OrderByDescending(m => m.Fecha)
            .ToListAsync();
    }

    public async Task AddAsync(Movimiento movimiento)
    {
        await _context.Movimientos.AddAsync(movimiento);
        await _context.SaveChangesAsync();
    }
}