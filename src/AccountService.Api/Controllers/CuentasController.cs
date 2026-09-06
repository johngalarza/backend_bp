using AccountService.Application.DTOs;
using AccountService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Api.Controllers;

[ApiController]
[Route("api/cuentas")]
public class CuentasController : ControllerBase
{
    private readonly ICuentaService _service;

    public CuentasController(ICuentaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cuentas = await _service.GetAllAsync();

        return Ok(cuentas);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var cuenta = await _service.GetByIdAsync(id);

        if (cuenta is null)
        {
            return NotFound(new
            {
                mensaje = "Cuenta no encontrada."
            });
        }

        return Ok(cuenta);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateCuentaDto dto)
    {
        var cuenta = await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = cuenta.Id },
            cuenta
        );
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateCuentaDto dto)
    {
        var cuenta = await _service.UpdateAsync(id, dto);

        if (cuenta is null)
        {
            return NotFound(new
            {
                mensaje = "Cuenta no encontrada."
            });
        }

        return Ok(cuenta);
    }
}