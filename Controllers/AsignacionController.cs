using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Helpers;
using Obligatorio_Programacion.Service;

namespace Obligatorio_Programacion.Controllers
{
    [ApiController]
    [Route("api/asignacion")]
    public class AsignacionController : ControllerBase
    {
        private readonly AsignacionService _asignacionService;
        private readonly AuditoriaService _auditoriaService;

        public AsignacionController(AsignacionService asignacionService, AuditoriaService auditoriaService)
        {
            _asignacionService = asignacionService;
            _auditoriaService = auditoriaService;
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var asignaciones = _asignacionService.ObtenerTodos();
            return Ok(asignaciones);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var asignacion = _asignacionService.ObtenerPorId(id);

            if (asignacion == null)
            {
                return NotFound("Asignación no encontrada.");
            }

            return Ok(asignacion);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] Asignacion asignacion)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede asignar empleados a una obra.");
            }

            if (asignacion.IdUsuario <= 0 || asignacion.IdObra <= 0)
            {
                return BadRequest("Debe seleccionar una obra y un empleado válidos.");
            }

            var existente = _asignacionService.ObtenerPorUsuarioYObra(asignacion.IdUsuario, asignacion.IdObra);
            if (existente != null)
            {
                return BadRequest("Ese empleado ya está asignado a esta obra.");
            }

            try
            {
                _asignacionService.Add(asignacion);

                var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
                _auditoriaService.RegistrarCreacion(0, "Asignaciones", asignacion.IdAsignacion, asignacion, direccionIP);

                return Ok(asignacion);
            }
            catch (DbUpdateException)
            {
                return BadRequest("No se pudo guardar la asignación. Revisá que la obra y el empleado existan.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Actualizar(int id, [FromBody] Asignacion asignacion)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede modificar asignaciones.");
            }

            var asignacionAnterior = _asignacionService.ObtenerPorId(id);
            if (asignacionAnterior == null)
            {
                return NotFound("Asignación no encontrada.");
            }

            var actualizado = _asignacionService.Update(id, asignacion);

            if (actualizado == null)
            {
                return NotFound("Asignación no encontrada.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarModificacion(0, "Asignaciones", id, asignacionAnterior, actualizado, direccionIP);

            return Ok(actualizado);
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede eliminar asignaciones.");
            }

            var asignacionAnterior = _asignacionService.ObtenerPorId(id);
            if (asignacionAnterior == null)
            {
                return NotFound("Asignación no encontrada.");
            }

            var eliminado = _asignacionService.Delete(id);

            if (!eliminado)
            {
                return NotFound("Asignación no encontrada.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarEliminacion(0, "Asignaciones", id, asignacionAnterior, direccionIP);

            return Ok("Asignación eliminada correctamente.");
        }
    }
}
