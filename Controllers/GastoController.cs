using Microsoft.AspNetCore.Mvc;
using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Helpers;
using Obligatorio_Programacion.Service;

namespace Obligatorio_Programacion.Controllers
{
    [ApiController]
    [Route("api/gasto")]
    public class GastoController : ControllerBase
    {
        private readonly GastoService _gastoService;
        private readonly AuditoriaService _auditoriaService;

        public GastoController(GastoService gastoService, AuditoriaService auditoriaService)
        {
            _gastoService = gastoService;
            _auditoriaService = auditoriaService;
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var gastos = _gastoService.ObtenerTodos();
            return Ok(gastos);
        }

        [HttpGet("{id}")] 
        public IActionResult ObtenerPorId(int id)
        {
            var gasto = _gastoService.ObtenerPorId(id);

            if (gasto == null)
            {
                return NotFound("Registro no encontrado.");
            }

            return Ok(gasto);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] Gasto gasto)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acción.");
            }

            _gastoService.Add(gasto);

            // Registrar en auditoría
            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarCreacion(
                idUsuario: 0, // Admin (se puede mejorar con autenticación real)
                tabla: "Gastos",
                idRegistro: gasto.IdGasto,
                datosNuevos: gasto,
                direccionIP: direccionIP
            );

            return Ok(gasto);
        }

        [HttpPut("{id}")] 
        public IActionResult Actualizar(int id, [FromBody] Gasto gasto)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acción.");
            }

            // Obtener datos anteriores
            var gastoAnterior = _gastoService.ObtenerPorId(id);
            if (gastoAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var actualizado = _gastoService.Update(id, gasto);

            if (actualizado == null)
            {
                return NotFound("Registro no encontrado.");
            }

            // Registrar en auditoría
            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarModificacion(
                idUsuario: 0,
                tabla: "Gastos",
                idRegistro: id,
                datosAnteriores: gastoAnterior,
                datosNuevos: actualizado,
                direccionIP: direccionIP
            );

            return Ok(actualizado);
        }

        [HttpDelete("{id}")] 
        public IActionResult Eliminar(int id)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acción.");
            }

            // Obtener datos antes de eliminar
            var gastoAnterior = _gastoService.ObtenerPorId(id);
            if (gastoAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var eliminado = _gastoService.Delete(id);

            if (!eliminado)
            {
                return NotFound("Registro no encontrado.");
            }

            // Registrar en auditoría
            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarEliminacion(
                idUsuario: 0,
                tabla: "Gastos",
                idRegistro: id,
                datosAnteriores: gastoAnterior,
                direccionIP: direccionIP
            );

            return Ok("Registro eliminado correctamente.");
        }
    }
}
