using Microsoft.AspNetCore.Mvc;
using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Helpers;
using Obligatorio_Programacion.Service;

namespace Obligatorio_Programacion.Controllers
{
    [ApiController]
    [Route("api/proveedor")]
    public class ProveedorController : ControllerBase
    {
        private readonly ProveedorService _proveedorService;
        private readonly AuditoriaService _auditoriaService;

        public ProveedorController(ProveedorService proveedorService, AuditoriaService auditoriaService)
        {
            _proveedorService = proveedorService;
            _auditoriaService = auditoriaService;
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var proveedors = _proveedorService.ObtenerTodos();
            return Ok(proveedors);
        }

        [HttpGet("{id}")] 
        public IActionResult ObtenerPorId(int id)
        {
            var proveedor = _proveedorService.ObtenerPorId(id);

            if (proveedor == null)
            {
                return NotFound("Registro no encontrado.");
            }

            return Ok(proveedor);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] Proveedor proveedor)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acción.");
            }

            _proveedorService.Add(proveedor);

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarCreacion(0, "Proveedores", proveedor.IdProveedor, proveedor, direccionIP);

            return Ok(proveedor);
        }

        [HttpPut("{id}")] 
        public IActionResult Actualizar(int id, [FromBody] Proveedor proveedor)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acción.");
            }

            var proveedorAnterior = _proveedorService.ObtenerPorId(id);
            if (proveedorAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var actualizado = _proveedorService.Update(id, proveedor);

            if (actualizado == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarModificacion(0, "Proveedores", id, proveedorAnterior, actualizado, direccionIP);

            return Ok(actualizado);
        }

        [HttpDelete("{id}")] 
        public IActionResult Eliminar(int id)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acción.");
            }

            var proveedorAnterior = _proveedorService.ObtenerPorId(id);
            if (proveedorAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var eliminado = _proveedorService.Delete(id);

            if (!eliminado)
            {
                return NotFound("Registro no encontrado.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarEliminacion(0, "Proveedores", id, proveedorAnterior, direccionIP);

            return Ok("Registro eliminado correctamente.");
        }
    }
}
