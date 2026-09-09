using AccountService.Application.DTOs;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Domain.Repositories;

namespace AccountService.Application.Services;

public class MovimientoService : IMovimientoService
{
    private readonly ICuentaRepository _cuentaRepository;
    private readonly IMovimientoRepository _movimientoRepository;

    private readonly IUnitOfWork _unitOfWork;

    public MovimientoService(
        ICuentaRepository cuentaRepository,
        IMovimientoRepository movimientoRepository,
        IUnitOfWork unitOfWork)
    {
        _cuentaRepository = cuentaRepository;
        _movimientoRepository = movimientoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<MovimientoResponseDto>>
        GetByCuentaIdAsync(Guid cuentaId)
    {
        var movimientos =
            await _movimientoRepository
                .GetByCuentaIdAsync(cuentaId);

        return movimientos.Select(ToResponseDto);
    }

    public async Task<MovimientoResponseDto> CreateAsync(CreateMovimientoDto dto)
    {
        if (dto.Valor == 0)
        {
            throw new ArgumentException(
                "El valor del movimiento no puede ser cero.");
        }

        Movimiento? movimiento = null;

        await _unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            var cuenta =
                await _cuentaRepository.GetByIdAsync(dto.CuentaId);

            if (cuenta is null)
            {
                throw new KeyNotFoundException(
                    "Cuenta no encontrada.");
            }

            cuenta.AplicarMovimiento(dto.Valor);

            movimiento = new Movimiento(
                dto.Valor,
                cuenta.SaldoDisponible,
                cuenta.Id
            );

            await _cuentaRepository.UpdateAsync(cuenta);

            await _movimientoRepository.AddAsync(movimiento);
        });

        return ToResponseDto(movimiento!);
    }

    private static MovimientoResponseDto ToResponseDto(
        Movimiento movimiento)
    {
        return new MovimientoResponseDto(
            movimiento.Id,
            movimiento.Fecha,
            movimiento.TipoMovimiento,
            movimiento.Valor,
            movimiento.Saldo,
            movimiento.CuentaId
        );
    }
}