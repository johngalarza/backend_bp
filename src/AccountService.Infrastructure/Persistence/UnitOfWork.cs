using AccountService.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AccountDbContext _context;

    public UnitOfWork(AccountDbContext context)
    {
        _context = context;
    }

    public async Task ExecuteInTransactionAsync(
        Func<Task> action)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
            await action();

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}