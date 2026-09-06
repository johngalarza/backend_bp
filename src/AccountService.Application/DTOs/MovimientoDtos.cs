namespace AccountService.Application.DTOs;

public record CreateMovimientoDto(
    Guid CuentaId,
    decimal Valor
);

public record MovimientoResponseDto(
    Guid Id,
    DateTime Fecha,
    string TipoMovimiento,
    decimal Valor,
    decimal Saldo,
    Guid CuentaId
);