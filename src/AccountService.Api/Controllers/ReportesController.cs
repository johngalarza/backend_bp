using AccountService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Api.Controllers;

[ApiController]
[Route("api/reportes")]
public class ReportesController : ControllerBase
{
    private readonly IReporteService _service;

    public ReportesController(IReporteService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Generar(
        [FromQuery] string clienteId,
        [FromQuery] DateTime fechaInicio,
        [FromQuery] DateTime fechaFin)
    {
        if (string.IsNullOrWhiteSpace(clienteId))
        {
            return BadRequest(new
            {
                mensaje = "El clienteId es obligatorio."
            });
        }

        var reporte = await _service.GenerarAsync(
            clienteId,
            fechaInicio,
            fechaFin
        );

        return Ok(reporte);
    }
}