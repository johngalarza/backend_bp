namespace Shared.Contracts.Events;

public record ClienteCreadoEvent(
    Guid Id,
    string ClienteId,
    string Nombre,
    string Identificacion,
    bool Estado
);