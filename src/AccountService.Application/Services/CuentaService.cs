using AccountService.Application.DTOs;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Domain.Repositories;

namespace AccountService.Application.Services;

public class CuentaService : ICuentaService
{
    private readonly ICuentaRepository _repository;

    public CuentaService(ICuentaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CuentaResponseDto>> GetAllAsync()
    {
        var cuentas = await _repository.GetAllAsync();

        return cuentas.Select(ToResponseDto);
    }

    public async Task<CuentaResponseDto?> GetByIdAsync(Guid id)
    {
        var cuenta = await _repository.GetByIdAsync(id);

        return cuenta is null
            ? null
            : ToResponseDto(cuenta);
    }

    public async Task<CuentaResponseDto> CreateAsync(
        CreateCuentaDto dto)
    {
        var existingCuenta =
            await _repository.GetByNumeroCuentaAsync(
                dto.NumeroCuenta);

        if (existingCuenta is not null)
        {
            throw new InvalidOperationException(
                "El número de cuenta ya está registrado.");
        }

        var cuenta = new Cuenta(
            dto.NumeroCuenta,
            dto.TipoCuenta,
            dto.SaldoInicial,
            dto.ClienteId
        );

        await _repository.AddAsync(cuenta);

        return ToResponseDto(cuenta);
    }

    public async Task<CuentaResponseDto?> UpdateAsync(
        Guid id,
        UpdateCuentaDto dto)
    {
        var cuenta = await _repository.GetByIdAsync(id);

        if (cuenta is null)
            return null;

        cuenta.Actualizar(
            dto.TipoCuenta,
            dto.Estado
        );

        await _repository.UpdateAsync(cuenta);

        return ToResponseDto(cuenta);
    }

    private static CuentaResponseDto ToResponseDto(
        Cuenta cuenta)
    {
        return new CuentaResponseDto(
            cuenta.Id,
            cuenta.NumeroCuenta,
            cuenta.TipoCuenta,
            cuenta.SaldoInicial,
            cuenta.SaldoDisponible,
            cuenta.Estado,
            cuenta.ClienteId
        );
    }
}