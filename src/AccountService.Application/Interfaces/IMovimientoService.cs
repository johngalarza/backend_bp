using AccountService.Application.DTOs;

namespace AccountService.Application.Interfaces;

public interface IMovimientoService
{
    Task<IEnumerable<MovimientoResponseDto>> GetByCuentaIdAsync(
        Guid cuentaId);

    Task<MovimientoResponseDto> CreateAsync(
        CreateMovimientoDto dto);
}