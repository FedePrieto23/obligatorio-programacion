using Microsoft.AspNetCore.Mvc;
using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Helpers;
using Obligatorio_Programacion.Service;

namespace Obligatorio_Programacion.Controllers
{
    [ApiController]
    [Route("api/oficio")]
    public class OficioController : ControllerBase
    {
        private readonly OficioService _oficioService;
        private readonly AuditoriaService _auditoriaService;

        public OficioController(OficioService oficioService, AuditoriaService auditoriaService)
        {
            _oficioService = oficioService;
            _auditoriaService = auditoriaService;
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var oficios = _oficioService.ObtenerTodos();
            return Ok(oficios);
        }

        [HttpGet("{id}")] 
        public IActionResult ObtenerPorId(int id)
        {
            var oficio = _oficioService.ObtenerPorId(id);

            if (oficio == null)
            {
                return NotFound("Registro no encontrado.");
            }

            return Ok(oficio);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] Oficio oficio)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acciÃ³n.");
            }

            _oficioService.Add(oficio);

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarCreacion(0, "Oficios", oficio.IdOficio, oficio, direccionIP);

            return Ok(oficio);
        }

        [HttpPut("{id}")] 
        public IActionResult Actualizar(int id, [FromBody] Oficio oficio)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acciÃ³n.");
            }

            var oficioAnterior = _oficioService.ObtenerPorId(id);
            if (oficioAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var actualizado = _oficioService.Update(id, oficio);

            if (actualizado == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarModificacion(0, "Oficios", id, oficioAnterior, actualizado, direccionIP);

            return Ok(actualizado);
        }

        [HttpDelete("{id}")] 
        public IActionResult Eliminar(int id)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acciÃ³n.");
            }

            var oficioAnterior = _oficioService.ObtenerPorId(id);
            if (oficioAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var eliminado = _oficioService.Delete(id);

            if (!eliminado)
            {
                return NotFound("Registro no encontrado.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarEliminacion(0, "Oficios", id, oficioAnterior, direccionIP);

            return Ok("Registro eliminado correctamente.");
        }
    }
}
