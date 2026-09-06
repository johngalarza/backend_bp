using CustomerService.Application.DTOs;
using CustomerService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace CustomerService.Api.Controllers;

[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _service;
    private readonly IValidator<CreateClienteDto> _validator;

    public ClientesController(
        IClienteService service,
        IValidator<CreateClienteDto> validator)
    {
        _service = service;
        _validator = validator;
    }

    // GET /api/clientes
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clientes = await _service.GetAllAsync();

        return Ok(clientes);
    }

    // GET /api/clientes/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var cliente = await _service.GetByIdAsync(id);

        if (cliente is null)
            return NotFound(new
            {
                mensaje = "Cliente no encontrado."
            });

        return Ok(cliente);
    }

    // POST /api/clientes
    [HttpPost]
    public async Task<IActionResult> Create(CreateClienteDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            var errores = validationResult
                .Errors
                .GroupBy(error => error.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group
                        .Select(error => error.ErrorMessage)
                        .ToArray()
                );

            return BadRequest(new
            {
                mensaje = "La solicitud contiene errores de validación.",
                errores
            });
        }

        try
        {
            var cliente = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = cliente.Id },
                cliente
            );
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                mensaje = ex.Message
            });
        }
    }

    // PUT /api/clientes/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateClienteDto dto)
    {
        var cliente = await _service.UpdateAsync(id, dto);

        if (cliente is null)
            return NotFound(new
            {
                mensaje = "Cliente no encontrado."
            });

        return Ok(cliente);
    }

    // DELETE /api/clientes/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound(new
            {
                mensaje = "Cliente no encontrado."
            });

        return NoContent();
    }
}