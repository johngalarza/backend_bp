using AccountService.Application.DTOs;

namespace AccountService.Application.Interfaces;

public interface ICuentaService
{
    Task<IEnumerable<CuentaResponseDto>> GetAllAsync();

    Task<CuentaResponseDto?> GetByIdAsync(Guid id);

    Task<CuentaResponseDto> CreateAsync(
        CreateCuentaDto dto);

    Task<CuentaResponseDto?> UpdateAsync(
        Guid id,
        UpdateCuentaDto dto);
}