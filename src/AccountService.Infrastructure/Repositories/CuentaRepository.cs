using AccountService.Domain.Entities;
using AccountService.Domain.Repositories;
using AccountService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Infrastructure.Repositories;

public class CuentaRepository : ICuentaRepository
{
    private readonly AccountDbContext _context;

    public CuentaRepository(AccountDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cuenta>> GetAllAsync()
    {
        return await _context.Cuentas
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Cuenta?> GetByIdAsync(Guid id)
    {
        return await _context.Cuentas
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Cuenta?> GetByNumeroCuentaAsync(
        string numeroCuenta)
    {
        return await _context.Cuentas
            .FirstOrDefaultAsync(
                c => c.NumeroCuenta == numeroCuenta);
    }

    public async Task AddAsync(Cuenta cuenta)
    {
        await _context.Cuentas.AddAsync(cuenta);
        await _context.SaveChangesAsync();
    }

    public Task UpdateAsync(Cuenta cuenta)
    {
        _context.Cuentas.Update(cuenta);

        return Task.CompletedTask;
    }
}