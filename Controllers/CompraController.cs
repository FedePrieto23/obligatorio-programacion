using Microsoft.AspNetCore.Mvc;
using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Helpers;
using Obligatorio_Programacion.Service;

namespace Obligatorio_Programacion.Controllers
{
    [ApiController]
    [Route("api/compra")]
    public class CompraController : ControllerBase
    {
        private readonly CompraService _compraService;
        private readonly AuditoriaService _auditoriaService;

        public CompraController(CompraService compraService, AuditoriaService auditoriaService)
        {
            _compraService = compraService;
            _auditoriaService = auditoriaService;
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var compras = _compraService.ObtenerTodos();
            return Ok(compras);
        }

        [HttpGet("{id}")] 
        public IActionResult ObtenerPorId(int id)
        {
            var compra = _compraService.ObtenerPorId(id);

            if (compra == null)
            {
                return NotFound("Registro no encontrado.");
            }

            return Ok(compra);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] Compra compra)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acciÃ³n.");
            }

            _compraService.Add(compra);

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarCreacion(0, "Compras", compra.IdCompra, compra, direccionIP);

            return Ok(compra);
        }

        [HttpPut("{id}")] 
        public IActionResult Actualizar(int id, [FromBody] Compra compra)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acciÃ³n.");
            }

            var compraAnterior = _compraService.ObtenerPorId(id);
            if (compraAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var actualizado = _compraService.Update(id, compra);

            if (actualizado == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarModificacion(0, "Compras", id, compraAnterior, actualizado, direccionIP);

            return Ok(actualizado);
        }

        [HttpDelete("{id}")] 
        public IActionResult Eliminar(int id)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acciÃ³n.");
            }

            var compraAnterior = _compraService.ObtenerPorId(id);
            if (compraAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var eliminado = _compraService.Delete(id);

            if (!eliminado)
            {
                return NotFound("Registro no encontrado.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarEliminacion(0, "Compras", id, compraAnterior, direccionIP);

            return Ok("Registro eliminado correctamente.");
        }
    }
}
