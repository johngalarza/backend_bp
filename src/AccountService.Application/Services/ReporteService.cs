using AccountService.Application.DTOs;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Domain.Repositories;

namespace AccountService.Application.Services;

public class ReporteService : IReporteService
{
    private readonly ICuentaRepository _cuentaRepository;
    private readonly IMovimientoRepository _movimientoRepository;

    public ReporteService(
        ICuentaRepository cuentaRepository,
        IMovimientoRepository movimientoRepository)
    {
        _cuentaRepository = cuentaRepository;
        _movimientoRepository = movimientoRepository;
    }

    public async Task<ReporteResponseDto> GenerarAsync(
        string clienteId,
        DateTime fechaInicio,
        DateTime fechaFin)
    {
        if (fechaInicio > fechaFin)
        {
            throw new ArgumentException(
                "La fecha de inicio no puede ser mayor que la fecha de fin.");
        }

        var cuentas =
            (await _cuentaRepository
                .GetByClienteIdAsync(clienteId))
            .ToList();

        var cuentaIds = cuentas
            .Select(c => c.Id)
            .ToList();

        var movimientos = cuentaIds.Count == 0
            ? Enumerable.Empty<Movimiento>()
            : await _movimientoRepository
                .GetByCuentaIdsAsync(cuentaIds);

        var movimientosPorCuenta = movimientos
            .Where(m =>
                m.Fecha >= fechaInicio &&
                m.Fecha <= fechaFin)
            .GroupBy(m => m.CuentaId)
            .ToDictionary(
                group => group.Key,
                group => group
                    .Select(m => new ReporteMovimientoDto(
                        m.Fecha,
                        m.TipoMovimiento,
                        m.Valor,
                        m.Saldo
                    ))
                    .ToList()
            );

        var cuentasReporte = cuentas
            .Select(c =>
                new ReporteCuentaDto(
                    c.NumeroCuenta,
                    c.TipoCuenta,
                    c.SaldoInicial,
                    c.SaldoDisponible,
                    c.Estado,
                    movimientosPorCuenta.TryGetValue(
                        c.Id,
                        out var movimientosCuenta)
                        ? movimientosCuenta
                        : new List<ReporteMovimientoDto>()
                )
            )
            .ToList();

        return new ReporteResponseDto(
            clienteId,
            fechaInicio,
            fechaFin,
            cuentasReporte
        );
    }
}