namespace CustomerService.Application.DTOs;

public record CreateClienteDto(
    string Nombre,
    string Genero,
    int Edad,
    string Identificacion,
    string Direccion,
    string Telefono,
    string ClienteId,
    string Password
);

public record UpdateClienteDto(
    string Nombre,
    string Genero,
    int Edad,
    string Direccion,
    string Telefono,
    bool Estado
);

public record ClienteResponseDto(
    Guid Id,
    string Nombre,
    string Genero,
    int Edad,
    string Identificacion,
    string Direccion,
    string Telefono,
    string ClienteId,
    bool Estado
);