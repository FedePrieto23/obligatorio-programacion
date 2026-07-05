using Microsoft.EntityFrameworkCore;
using Obligatorio_Programacion.Data;
using Obligatorio_Programacion.Entity;

namespace Obligatorio_Programacion.Repository
{
    public class AsignacionRepository
    {
        private readonly AppDbContext _context;

        public AsignacionRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Asignacion> ObtenerTodos()
        {
            return _context.Asignaciones
                .Include(asignacion => asignacion.Obra)
                    .ThenInclude(obra => obra!.Cliente)
                .Include(asignacion => asignacion.Usuario)
                    .ThenInclude(usuario => usuario!.Oficio)
                .ToList();
        }

        public Asignacion? ObtenerPorId(int id)
        {
            return _context.Asignaciones
                .Include(asignacion => asignacion.Obra)
                    .ThenInclude(obra => obra!.Cliente)
                .Include(asignacion => asignacion.Usuario)
                    .ThenInclude(usuario => usuario!.Oficio)
                .FirstOrDefault(asignacion => asignacion.IdAsignacion == id);
        }

        public Asignacion? ObtenerPorUsuarioYObra(int idUsuario, int idObra)
        {
            return _context.Asignaciones
                .FirstOrDefault(asignacion => asignacion.IdUsuario == idUsuario && asignacion.IdObra == idObra);
        }

        public void Add(Asignacion asignacion)
        {
            asignacion.Usuario = null;
            asignacion.Obra = null;
            _context.Asignaciones.Add(asignacion);
            _context.SaveChanges();
        }

        public Asignacion? Update(int id, Asignacion datos)
        {
            var asignacion = _context.Asignaciones.Find(id);

            if (asignacion == null)
            {
                return null;
            }

            asignacion.IdUsuario = datos.IdUsuario;
            asignacion.IdObra = datos.IdObra;

            _context.SaveChanges();
            return asignacion;
        }

        public bool Delete(int id)
        {
            var asignacion = _context.Asignaciones.Find(id);

            if (asignacion == null)
            {
                return false;
            }

            _context.Asignaciones.Remove(asignacion);
            _context.SaveChanges();
            return true;
        }
    }
}
