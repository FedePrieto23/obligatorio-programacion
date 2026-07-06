using Microsoft.AspNetCore.Mvc;
using Obligatorio_Programacion.Service;
using Obligatorio_Programacion.Helpers;

namespace Obligatorio_Programacion.Controllers
{
    [ApiController]
    [Route("api/auditoria")]
    public class AuditoriaController : ControllerBase
    {
        private readonly AuditoriaService _auditoriaService;

        public AuditoriaController(AuditoriaService auditoriaService)
        {
            _auditoriaService = auditoriaService;
        }

        [HttpGet]
        public IActionResult ObtenerTodas()
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede ver el historial de auditorÃ­a.");
            }

            var auditorias = _auditoriaService.ObtenerTodas();
            return Ok(auditorias);
        }

        [HttpGet("tabla/{tabla}")]
        public IActionResult ObtenerPorTabla(string tabla)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede ver el historial de auditorÃ­a.");
            }

            var auditorias = _auditoriaService.ObtenerPorTabla(tabla);
            return Ok(auditorias);
        }

        [HttpGet("usuario/{idUsuario}")]
        public IActionResult ObtenerPorUsuario(int idUsuario)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede ver el historial de auditorÃ­a.");
            }

            var auditorias = _auditoriaService.ObtenerPorUsuario(idUsuario);
            return Ok(auditorias);
        }

        [HttpGet("registro/{tabla}/{idRegistro}")]
        public IActionResult ObtenerPorRegistro(string tabla, int idRegistro)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede ver el historial de auditorÃ­a.");
            }

            var auditorias = _auditoriaService.ObtenerPorRegistro(tabla, idRegistro);
            return Ok(auditorias);
        }
    }
}
