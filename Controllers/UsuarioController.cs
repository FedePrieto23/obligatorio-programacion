using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Helpers;
using Obligatorio_Programacion.Service;
using System.Text.Json;

namespace Obligatorio_Programacion.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly AuditoriaService _auditoriaService;

        public UsuarioController(UsuarioService usuarioService, AuditoriaService auditoriaService)
        {
            _usuarioService = usuarioService;
            _auditoriaService = auditoriaService;
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var usuarios = _usuarioService.ObtenerTodos();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var usuario = _usuarioService.ObtenerPorId(id);

            if (usuario == null)
            {
                return NotFound("Registro no encontrado.");
            }

            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] Usuario usuario)
        {
            // El registro de cuentas se hace desde la pantalla de inicio, por eso este POST queda público.
            // Las modificaciones y eliminaciones siguen reservadas al administrador.
            if (string.IsNullOrWhiteSpace(usuario.NombreUsuario) || string.IsNullOrWhiteSpace(usuario.EmailUsuario) || string.IsNullOrWhiteSpace(usuario.Contraseña))
            {
                return BadRequest("Nombre, email y contraseña son obligatorios.");
            }

            if (usuario.IdOficio <= 0)
            {
                return BadRequest("Debe seleccionar un oficio válido.");
            }

            var existe = _usuarioService.ObtenerTodos()
                .Any(u => u.EmailUsuario.ToLower() == usuario.EmailUsuario.ToLower());

            if (existe)
            {
                return BadRequest("Ya existe un usuario registrado con ese email.");
            }

            try
            {
                _usuarioService.Add(usuario);

                var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
                _auditoriaService.RegistrarCreacion(0, "Usuarios", usuario.IdUsuario, usuario, direccionIP);

                return Ok(usuario);
            }
            catch (DbUpdateException)
            {
                return BadRequest("No se pudo crear el usuario. Revisá que el oficio seleccionado exista.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Actualizar(int id, [FromBody] Usuario usuario)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acción.");
            }

            var usuarioAnterior = _usuarioService.ObtenerPorId(id);
            if (usuarioAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var actualizado = _usuarioService.Update(id, usuario);

            if (actualizado == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarModificacion(0, "Usuarios", id, usuarioAnterior, actualizado, direccionIP);

            return Ok(actualizado);
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acción.");
            }

            var eliminado = _usuarioService.Delete(id);

            if (!eliminado)
            {
                return NotFound("Registro no encontrado.");
            }

            return Ok("Registro eliminado correctamente.");
        }

        // Endpoint de debug para ver todos los usuarios
        [HttpGet("debug-usuarios")]
        public IActionResult DebugUsuarios()
        {
            try
            {
                var usuarios = _usuarioService.ObtenerTodos();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}