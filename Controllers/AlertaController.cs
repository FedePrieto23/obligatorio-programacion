using Microsoft.AspNetCore.Mvc;
using Obligatorio_Programacion.Service;

namespace Obligatorio_Programacion.Controllers
{
    [ApiController]
    [Route("api/alerta")]
    public class AlertaController : ControllerBase
    {
        private readonly AlertaService _alertaService;

        public AlertaController(AlertaService alertaService)
        {
            _alertaService = alertaService;
        }

        [HttpGet]
        public IActionResult ObtenerTodas()
        {
            var alertas = _alertaService.ObtenerTodas();
            return Ok(alertas);
        }

        [HttpGet("activas")]
        public IActionResult ObtenerActivas()
        {
            var alertas = _alertaService.ObtenerActivas();
            return Ok(alertas);
        }

        [HttpGet("obra/{idObra}")]
        public IActionResult ObtenerPorObra(int idObra)
        {
            var alertas = _alertaService.ObtenerPorObra(idObra);
            return Ok(alertas);
        }

        [HttpGet("contar-activas")]
        public IActionResult ContarActivas()
        {
            var count = _alertaService.ObtenerCountAlertasActivas();
            return Ok(new { activas = count });
        }

        [HttpPost("verificar")]
        public IActionResult VerificarYGenerar()
        {
            _alertaService.VerificarYGenerarAlertas();
            return Ok(new { mensaje = "Alertas verificadas y generadas" });
        }

        [HttpPut("resolver/{idAlerta}")]
        public IActionResult Resolver(int idAlerta)
        {
            _alertaService.ResolverAlerta(idAlerta);
            return Ok(new { mensaje = "Alerta resuelta" });
        }
    }
}
