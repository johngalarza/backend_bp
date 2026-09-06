namespace AccountService.Application.DTOs;

public record CreateCuentaDto(
    string NumeroCuenta,
    string TipoCuenta,
    decimal SaldoInicial,
    string ClienteId
);

public record UpdateCuentaDto(
    string TipoCuenta,
    bool Estado
);

public record CuentaResponseDto(
    Guid Id,
    string NumeroCuenta,
    string TipoCuenta,
    decimal SaldoInicial,
    decimal SaldoDisponible,
    bool Estado,
    string ClienteId
);