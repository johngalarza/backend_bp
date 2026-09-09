using Shared.Contracts.Events;

namespace CustomerService.Application.Interfaces;

public interface IClienteEventPublisher
{
    Task PublishClienteCreadoAsync(ClienteCreadoEvent evento);
}