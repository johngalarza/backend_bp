namespace AccountService.Application.DTOs;

public record ReporteCuentaDto(
    string NumeroCuenta,
    string TipoCuenta,
    decimal SaldoInicial,
    decimal SaldoDisponible,
    bool Estado,
    List<ReporteMovimientoDto> Movimientos
);

public record ReporteMovimientoDto(
    DateTime Fecha,
    string TipoMovimiento,
    decimal Valor,
    decimal Saldo
);

public record ReporteResponseDto(
    string ClienteId,
    DateTime FechaInicio,
    DateTime FechaFin,
    List<ReporteCuentaDto> Cuentas
);