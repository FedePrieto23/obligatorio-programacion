using Microsoft.AspNetCore.Mvc;
using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Helpers;
using Obligatorio_Programacion.Service;

namespace Obligatorio_Programacion.Controllers
{
    [ApiController]
    [Route("api/tipogasto")]
    public class TipoGastoController : ControllerBase
    {
        private readonly TipoGastoService _tipoGastoService;
        private readonly AuditoriaService _auditoriaService;

        public TipoGastoController(TipoGastoService tipoGastoService, AuditoriaService auditoriaService)
        {
            _tipoGastoService = tipoGastoService;
            _auditoriaService = auditoriaService;
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var tipoGastos = _tipoGastoService.ObtenerTodos();
            return Ok(tipoGastos);
        }

        [HttpGet("{id}")] 
        public IActionResult ObtenerPorId(int id)
        {
            var tipoGasto = _tipoGastoService.ObtenerPorId(id);

            if (tipoGasto == null)
            {
                return NotFound("Registro no encontrado.");
            }

            return Ok(tipoGasto);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] TipoGasto tipoGasto)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acción.");
            }

            _tipoGastoService.Add(tipoGasto);

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarCreacion(0, "TiposGasto", tipoGasto.IdTipoGasto, tipoGasto, direccionIP);

            return Ok(tipoGasto);
        }

        [HttpPut("{id}")] 
        public IActionResult Actualizar(int id, [FromBody] TipoGasto tipoGasto)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acción.");
            }

            var tipoGastoAnterior = _tipoGastoService.ObtenerPorId(id);
            if (tipoGastoAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var actualizado = _tipoGastoService.Update(id, tipoGasto);

            if (actualizado == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarModificacion(0, "TiposGasto", id, tipoGastoAnterior, actualizado, direccionIP);

            return Ok(actualizado);
        }

        [HttpDelete("{id}")] 
        public IActionResult Eliminar(int id)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acción.");
            }

            var tipoGastoAnterior = _tipoGastoService.ObtenerPorId(id);
            if (tipoGastoAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var eliminado = _tipoGastoService.Delete(id);

            if (!eliminado)
            {
                return NotFound("Registro no encontrado.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarEliminacion(0, "TiposGasto", id, tipoGastoAnterior, direccionIP);

            return Ok("Registro eliminado correctamente.");
        }
    }
}
