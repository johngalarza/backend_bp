namespace AccountService.Application.Interfaces;

public interface IUnitOfWork
{
    Task ExecuteInTransactionAsync(
        Func<Task> action);
}