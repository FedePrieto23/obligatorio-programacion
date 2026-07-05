using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Data;
using System.Text.Json;

namespace Obligatorio_Programacion.Service
{
    public class AuditoriaService
    {
        private readonly AppDbContext _context;

        public AuditoriaService(AppDbContext context)
        {
            _context = context;
        }

        public List<Auditoria> ObtenerTodas()
        {
            return _context.Auditorias.OrderByDescending(a => a.FechaHora).ToList();
        }

        public List<Auditoria> ObtenerPorTabla(string tabla)
        {
            return _context.Auditorias
                .Where(a => a.Tabla == tabla)
                .OrderByDescending(a => a.FechaHora)
                .ToList();
        }

        public List<Auditoria> ObtenerPorUsuario(int idUsuario)
        {
            return _context.Auditorias
                .Where(a => a.IdUsuario == idUsuario)
                .OrderByDescending(a => a.FechaHora)
                .ToList();
        }

        public List<Auditoria> ObtenerPorRegistro(string tabla, int idRegistro)
        {
            return _context.Auditorias
                .Where(a => a.Tabla == tabla && a.IdRegistro == idRegistro)
                .OrderByDescending(a => a.FechaHora)
                .ToList();
        }

        public void RegistrarCreacion(int idUsuario, string tabla, int idRegistro, object datosNuevos, string? direccionIP = null)
        {
            var auditoria = new Auditoria
            {
                IdUsuario = idUsuario,
                Accion = "Create",
                Tabla = tabla,
                IdRegistro = idRegistro,
                DescripcionCambio = $"Creación de registro en {tabla}",
                FechaHora = DateTime.UtcNow,
                DatosAnteriores = string.Empty,
                DatosNuevos = JsonSerializer.Serialize(datosNuevos),
                DireccionIP = direccionIP ?? string.Empty
            };

            _context.Auditorias.Add(auditoria);
            _context.SaveChanges();
        }

        public void RegistrarModificacion(int idUsuario, string tabla, int idRegistro, object datosAnteriores, object datosNuevos, string? direccionIP = null)
        {
            var auditoria = new Auditoria
            {
                IdUsuario = idUsuario,
                Accion = "Update",
                Tabla = tabla,
                IdRegistro = idRegistro,
                DescripcionCambio = $"Modificación de registro en {tabla}",
                FechaHora = DateTime.UtcNow,
                DatosAnteriores = JsonSerializer.Serialize(datosAnteriores),
                DatosNuevos = JsonSerializer.Serialize(datosNuevos),
                DireccionIP = direccionIP ?? string.Empty
            };

            _context.Auditorias.Add(auditoria);
            _context.SaveChanges();
        }

        public void RegistrarEliminacion(int idUsuario, string tabla, int idRegistro, object datosAnteriores, string? direccionIP = null)
        {
            var auditoria = new Auditoria
            {
                IdUsuario = idUsuario,
                Accion = "Delete",
                Tabla = tabla,
                IdRegistro = idRegistro,
                DescripcionCambio = $"Eliminación de registro en {tabla}",
                FechaHora = DateTime.UtcNow,
                DatosAnteriores = JsonSerializer.Serialize(datosAnteriores),
                DatosNuevos = string.Empty,
                DireccionIP = direccionIP ?? string.Empty
            };

            _context.Auditorias.Add(auditoria);
            _context.SaveChanges();
        }
    }
}
