using Microsoft.AspNetCore.Mvc;
using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Helpers;
using Obligatorio_Programacion.Service;
using Microsoft.EntityFrameworkCore;

namespace Obligatorio_Programacion.Controllers
{
    [ApiController]
    [Route("api/obra")]
    public class ObraController : ControllerBase
    {
        private readonly ObraService _obraService;
        private readonly AuditoriaService _auditoriaService;

        public ObraController(ObraService obraService, AuditoriaService auditoriaService)
        {
            _obraService = obraService;
            _auditoriaService = auditoriaService;
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var obras = _obraService.ObtenerTodos();
            return Ok(obras);
        }

        [HttpGet("{id}")] 
        public IActionResult ObtenerPorId(int id)
        {
            var obra = _obraService.ObtenerPorId(id);

            if (obra == null)
            {
                return NotFound("Registro no encontrado.");
            }

            return Ok(obra);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] Obra obra)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acciÃ³n.");
            }

            if (obra.IdCliente <= 0)
            {
                return BadRequest("Debe seleccionar un cliente vÃ¡lido.");
            }

            try
            {
                _obraService.Add(obra);

                var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
                _auditoriaService.RegistrarCreacion(0, "Obras", obra.IdObra, obra, direccionIP);

                return Ok(obra);
            }
            catch (DbUpdateException)
            {
                return BadRequest("No se pudo guardar la obra. RevisÃ¡ que el cliente seleccionado exista en la tabla Clientes.");
            }
        }

        [HttpPut("{id}")] 
        public IActionResult Actualizar(int id, [FromBody] Obra obra)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acciÃ³n.");
            }

            var obraAnterior = _obraService.ObtenerPorId(id);
            if (obraAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var actualizado = _obraService.Update(id, obra);

            if (actualizado == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarModificacion(0, "Obras", id, obraAnterior, actualizado, direccionIP);

            return Ok(actualizado);
        }

        [HttpDelete("{id}")] 
        public IActionResult Eliminar(int id)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acciÃ³n.");
            }

            var obraAnterior = _obraService.ObtenerPorId(id);
            if (obraAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var eliminado = _obraService.Delete(id);

            if (!eliminado)
            {
                return NotFound("Registro no encontrado.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarEliminacion(0, "Obras", id, obraAnterior, direccionIP);

            return Ok("Registro eliminado correctamente.");
        }
    }
}
