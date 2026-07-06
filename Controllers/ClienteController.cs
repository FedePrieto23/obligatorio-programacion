using Microsoft.AspNetCore.Mvc;
using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Helpers;
using Obligatorio_Programacion.Service;

namespace Obligatorio_Programacion.Controllers
{
    [ApiController]
    [Route("api/cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;
        private readonly AuditoriaService _auditoriaService;

        public ClienteController(ClienteService clienteService, AuditoriaService auditoriaService)
        {
            _clienteService = clienteService;
            _auditoriaService = auditoriaService;
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var clientes = _clienteService.ObtenerTodos();
            return Ok(clientes);
        }

        [HttpGet("{id}")] 
        public IActionResult ObtenerPorId(int id)
        {
            var cliente = _clienteService.ObtenerPorId(id);

            if (cliente == null)
            {
                return NotFound("Registro no encontrado.");
            }

            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] Cliente cliente)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acciÃ³n.");
            }

            _clienteService.Add(cliente);

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarCreacion(0, "Clientes", cliente.IdCliente, cliente, direccionIP);

            return Ok(cliente);
        }

        [HttpPut("{id}")] 
        public IActionResult Actualizar(int id, [FromBody] Cliente cliente)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acciÃ³n.");
            }

            var clienteAnterior = _clienteService.ObtenerPorId(id);
            if (clienteAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var actualizado = _clienteService.Update(id, cliente);

            if (actualizado == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarModificacion(0, "Clientes", id, clienteAnterior, actualizado, direccionIP);

            return Ok(actualizado);
        }

        [HttpDelete("{id}")] 
        public IActionResult Eliminar(int id)
        {
            if (!PermisosHelper.EsAdministrador(Request))
            {
                return StatusCode(403, "Solo el administrador puede realizar esta acciÃ³n.");
            }

            var clienteAnterior = _clienteService.ObtenerPorId(id);
            if (clienteAnterior == null)
            {
                return NotFound("Registro no encontrado.");
            }

            var eliminado = _clienteService.Delete(id);

            if (!eliminado)
            {
                return NotFound("Registro no encontrado.");
            }

            var direccionIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            _auditoriaService.RegistrarEliminacion(0, "Clientes", id, clienteAnterior, direccionIP);

            return Ok("Registro eliminado correctamente.");
        }
    }
}
