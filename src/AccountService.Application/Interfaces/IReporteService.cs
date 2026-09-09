using AccountService.Application.DTOs;

namespace AccountService.Application.Interfaces;

public interface IReporteService
{
    Task<ReporteResponseDto> GenerarAsync(
        string clienteId,
        DateTime fechaInicio,
        DateTime fechaFin);
}