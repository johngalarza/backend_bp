using CustomerService.Application.DTOs;

namespace CustomerService.Application.Interfaces;

public interface IClienteService
{
    Task<IEnumerable<ClienteResponseDto>> GetAllAsync();

    Task<ClienteResponseDto?> GetByIdAsync(Guid id);

    Task<ClienteResponseDto> CreateAsync(CreateClienteDto dto);

    Task<ClienteResponseDto?> UpdateAsync(
        Guid id,
        UpdateClienteDto dto);

    Task<bool> DeleteAsync(Guid id);
}