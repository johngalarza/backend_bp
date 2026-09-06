using CustomerService.Application.DTOs;
using CustomerService.Application.Interfaces;
using CustomerService.Domain.Entities;
using CustomerService.Domain.Repositories;

namespace CustomerService.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ClienteResponseDto>> GetAllAsync()
    {
        var clientes = await _repository.GetAllAsync();

        return clientes.Select(ToResponseDto);
    }

    public async Task<ClienteResponseDto?> GetByIdAsync(Guid id)
    {
        var cliente = await _repository.GetByIdAsync(id);

        return cliente is null ? null : ToResponseDto(cliente);
    }

    public async Task<ClienteResponseDto> CreateAsync(CreateClienteDto dto)
    {
        var existingCliente = await _repository
            .GetByClienteIdAsync(dto.ClienteId);

        if (existingCliente is not null)
            throw new InvalidOperationException(
                "El ClienteId ya está registrado.");

        var existingIdentificacion = await _repository
            .GetByIdentificacionAsync(dto.Identificacion);

        if (existingIdentificacion is not null)
            throw new InvalidOperationException(
                "La identificación ya está registrada.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var cliente = new Cliente(
            dto.Nombre,
            dto.Genero,
            dto.Edad,
            dto.Identificacion,
            dto.Direccion,
            dto.Telefono,
            dto.ClienteId,
            passwordHash
        );

        await _repository.AddAsync(cliente);

        return ToResponseDto(cliente);
    }

    public async Task<ClienteResponseDto?> UpdateAsync(
        Guid id,
        UpdateClienteDto dto)
    {
        var cliente = await _repository.GetByIdAsync(id);

        if (cliente is null)
            return null;

        // Por ahora necesitamos métodos en Cliente
        // para modificar sus propiedades.
        cliente.ActualizarDatos(
            dto.Nombre,
            dto.Genero,
            dto.Edad,
            dto.Direccion,
            dto.Telefono,
            dto.Estado
        );

        await _repository.UpdateAsync(cliente);

        return ToResponseDto(cliente);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var cliente = await _repository.GetByIdAsync(id);

        if (cliente is null)
            return false;

        await _repository.DeleteAsync(cliente);

        return true;
    }

    private static ClienteResponseDto ToResponseDto(Cliente cliente)
    {
        return new ClienteResponseDto(
            cliente.Id,
            cliente.Nombre,
            cliente.Genero,
            cliente.Edad,
            cliente.Identificacion,
            cliente.Direccion,
            cliente.Telefono,
            cliente.ClienteId,
            cliente.Estado
        );
    }
}