using Microsoft.AspNetCore.Mvc;
using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Helpers;
using Obligatorio_Programacion.Service;

namespace Obligatorio_Programacion.Controllers
{
    [ApiController]
    [Route("api/tarea")]
    public class TareaController : ControllerBase
    {
        private readonly TareaService _tareaService;
        private readonly AuditoriaService _auditoriaService;

        public TareaController(TareaService tareaService, AuditoriaService auditoriaService)
        {
            _tareaService = tareaService;
            _auditoriaService = auditoriaService;
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var tareas = _tareaService.ObtenerTodos();
            return Ok(tareas);
        }

        [HttpGet("{id}")] 
        public IActionResult ObtenerPorId(int id)
        {
            var tarea = _tareaService.ObtenerPorId(id);

            if (tarea == null)
            {
                return NotFound("Registro no encontrado.");
            }

            return Ok(tarea);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] Tarea tarea)
        {
            if (!PermisosHelper.EsEmpleadoOAdministrador(Request))
            {
                return StatusCode(403, "Solo administradores o empleados pueden realizar esta acción.");
            }

            _tareaService.Add(tarea);

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarCreacion(0, "Tareas", tarea.IdTarea, tarea, direccionIP);

            return Ok(tarea);
        }

        [HttpPut("{id}")] 
        public IActionResult Actualizar(int id, [FromBody] Tarea tarea)
        {
            if (!PermisosHelper.EsEmpleadoOAdministrador(Request))
            {
                return StatusCode(403, "Solo administradores o empleados pueden realizar esta acción.");
            }

            var tareaAnterior = _tareaService.ObtenerPorId(id);
            if (tareaAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var actualizado = _tareaService.Update(id, tarea);

            if (actualizado == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarModificacion(0, "Tareas", id, tareaAnterior, actualizado, direccionIP);

            return Ok(actualizado);
        }

        [HttpDelete("{id}")] 
        public IActionResult Eliminar(int id)
        {
            if (!PermisosHelper.EsEmpleadoOAdministrador(Request))
            {
                return StatusCode(403, "Solo administradores o empleados pueden realizar esta acción.");
            }

            var tareaAnterior = _tareaService.ObtenerPorId(id);
            if (tareaAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var eliminado = _tareaService.Delete(id);

            if (!eliminado)
            {
                return NotFound("Registro no encontrado.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarEliminacion(0, "Tareas", id, tareaAnterior, direccionIP);

            return Ok("Registro eliminado correctamente.");
        }
    }
}
