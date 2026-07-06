using Microsoft.EntityFrameworkCore;
using Obligatorio_Programacion.Data;
using Obligatorio_Programacion.Entity;

namespace Obligatorio_Programacion.Repository
{
    public class ObraRepository
    {
        private readonly AppDbContext _context;

        public ObraRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Obra> ObtenerTodos()
        {
            return _context.Obras
                .Include(obra => obra.Cliente)
                .ToList();
        }

        public Obra? ObtenerPorId(int id)
        {
            return _context.Obras
                .Include(obra => obra.Cliente)
                .FirstOrDefault(obra => obra.IdObra == id);
        }

        public void Add(Obra obra)
        {
            obra.Cliente = null;
            _context.Obras.Add(obra);
            _context.SaveChanges();
        }

        public Obra? Update(int id, Obra datos)
        {
            var obra = _context.Obras.Find(id);

            if (obra == null)
            {
                return null;
            }

            obra.NombreObra = datos.NombreObra;
            obra.Presupuesto = datos.Presupuesto;
            obra.DireccionObra = datos.DireccionObra;
            obra.FechaInicio = datos.FechaInicio;
            obra.FechaFin = datos.FechaFin;
            obra.EstadoObra = datos.EstadoObra;
            obra.IdCliente = datos.IdCliente;

            _context.SaveChanges();
            return obra;
        }

        public bool Delete(int id)
        {
            var obra = _context.Obras.Find(id);

            if (obra == null)
            {
                return false;
            }

            _context.Obras.Remove(obra);
            _context.SaveChanges();
            return true;
        }
    }
}
