using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Helpers;
using Obligatorio_Programacion.Service;

namespace Obligatorio_Programacion.Controllers
{
    [ApiController]
    [Route("api/materialobra")]
    public class MaterialObraController : ControllerBase
    {
        private readonly MaterialObraService _materialObraService;
        private readonly AuditoriaService _auditoriaService;

        public MaterialObraController(MaterialObraService materialObraService, AuditoriaService auditoriaService)
        {
            _materialObraService = materialObraService;
            _auditoriaService = auditoriaService;
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var materialesObra = _materialObraService.ObtenerTodos();
            return Ok(materialesObra);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var materialObra = _materialObraService.ObtenerPorId(id);

            if (materialObra == null)
            {
                return NotFound("Registro no encontrado.");
            }

            return Ok(materialObra);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] MaterialObra materialObra)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acciÃ³n.");
            }

            if (materialObra.IdObra <= 0 || materialObra.IdMaterial <= 0)
            {
                return BadRequest("Debe seleccionar una obra y un material vÃ¡lidos.");
            }

            if (materialObra.CantidadMO <= 0)
            {
                return BadRequest("La cantidad debe ser mayor a cero.");
            }

            try
            {
                _materialObraService.Add(materialObra);

                var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
                _auditoriaService.RegistrarCreacion(0, "MaterialesObras", materialObra.IdMaterialObra, materialObra, direccionIP);

                return Ok(materialObra);
            }
            catch (DbUpdateException)
            {
                return BadRequest("No se pudo asociar el material. RevisÃ¡ que la obra y el material existan.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Actualizar(int id, [FromBody] MaterialObra materialObra)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acciÃ³n.");
            }

            var materialObraAnterior = _materialObraService.ObtenerPorId(id);
            if (materialObraAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var actualizado = _materialObraService.Update(id, materialObra);

            if (actualizado == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarModificacion(0, "MaterialesObras", id, materialObraAnterior, actualizado, direccionIP);

            return Ok(actualizado);
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acciÃ³n.");
            }

            var materialObraAnterior = _materialObraService.ObtenerPorId(id);
            if (materialObraAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var eliminado = _materialObraService.Delete(id);

            if (!eliminado)
            {
                return NotFound("Registro no encontrado.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarEliminacion(0, "MaterialesObras", id, materialObraAnterior, direccionIP);

            return Ok("Registro eliminado correctamente.");
        }
    }
}
