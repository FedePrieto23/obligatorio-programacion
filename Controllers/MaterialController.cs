using Microsoft.AspNetCore.Mvc;
using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Helpers;
using Obligatorio_Programacion.Service;

namespace Obligatorio_Programacion.Controllers
{
    [ApiController]
    [Route("api/material")]
    public class MaterialController : ControllerBase
    {
        private readonly MaterialService _materialService;
        private readonly AuditoriaService _auditoriaService;

        public MaterialController(MaterialService materialService, AuditoriaService auditoriaService)
        {
            _materialService = materialService;
            _auditoriaService = auditoriaService;
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var materials = _materialService.ObtenerTodos();
            return Ok(materials);
        }

        [HttpGet("{id}")] 
        public IActionResult ObtenerPorId(int id)
        {
            var material = _materialService.ObtenerPorId(id);

            if (material == null)
            {
                return NotFound("Registro no encontrado.");
            }

            return Ok(material);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] Material material)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acciÃ³n.");
            }

            _materialService.Add(material);

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarCreacion(0, "Materiales", material.IdMaterial, material, direccionIP);

            return Ok(material);
        }

        [HttpPut("{id}")] 
        public IActionResult Actualizar(int id, [FromBody] Material material)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acciÃ³n.");
            }

            var materialAnterior = _materialService.ObtenerPorId(id);
            if (materialAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var actualizado = _materialService.Update(id, material);

            if (actualizado == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarModificacion(0, "Materiales", id, materialAnterior, actualizado, direccionIP);

            return Ok(actualizado);
        }

        [HttpDelete("{id}")] 
        public IActionResult Eliminar(int id)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acciÃ³n.");
            }

            var materialAnterior = _materialService.ObtenerPorId(id);
            if (materialAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var eliminado = _materialService.Delete(id);

            if (!eliminado)
            {
                return NotFound("Registro no encontrado.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarEliminacion(0, "Materiales", id, materialAnterior, direccionIP);

            return Ok("Registro eliminado correctamente.");
        }
    }
}
