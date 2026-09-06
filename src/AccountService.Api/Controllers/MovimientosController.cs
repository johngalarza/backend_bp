using AccountService.Application.DTOs;
using AccountService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Api.Controllers;

[ApiController]
[Route("api/movimientos")]
public class MovimientosController : ControllerBase
{
    private readonly IMovimientoService _service;

    public MovimientosController(
        IMovimientoService service)
    {
        _service = service;
    }

    [HttpGet("cuenta/{cuentaId:guid}")]
    public async Task<IActionResult> GetByCuentaId(
        Guid cuentaId)
    {
        var movimientos =
            await _service.GetByCuentaIdAsync(cuentaId);

        return Ok(movimientos);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateMovimientoDto dto)
    {
        var movimiento =
            await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetByCuentaId),
            new { cuentaId = movimiento.CuentaId },
            movimiento
        );
    }
}