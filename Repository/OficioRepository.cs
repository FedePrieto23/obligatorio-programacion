using Obligatorio_Programacion.Data;
using Obligatorio_Programacion.Entity;

namespace Obligatorio_Programacion.Repository
{
    public class OficioRepository
    {
        private readonly AppDbContext _context;

        public OficioRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Oficio> ObtenerTodos()
        {
            return _context.Oficios.ToList();
        }

        public Oficio? ObtenerPorId(int id)
        {
            return _context.Oficios.Find(id);
        }

        public void Add(Oficio oficio)
        {
            _context.Oficios.Add(oficio);
            _context.SaveChanges();
        }

        public Oficio? Update(int id, Oficio datos)
        {
            var oficio = _context.Oficios.Find(id);

            if (oficio == null)
            {
                return null;
            }

            oficio.NombreOficio = datos.NombreOficio;
            oficio.descripcionOficio = datos.descripcionOficio;

            _context.SaveChanges();
            return oficio;
        }

        public bool Delete(int id)
        {
            var oficio = _context.Oficios.Find(id);

            if (oficio == null)
            {
                return false;
            }

            _context.Oficios.Remove(oficio);
            _context.SaveChanges();
            return true;
        }
    }
}
