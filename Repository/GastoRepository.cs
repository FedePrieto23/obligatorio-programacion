using Microsoft.EntityFrameworkCore;
using Obligatorio_Programacion.Data;
using Obligatorio_Programacion.Entity;

namespace Obligatorio_Programacion.Repository
{
    public class GastoRepository
    {
        private readonly AppDbContext _context;

        public GastoRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Gasto> ObtenerTodos()
        {
            return _context.Gastos
                .Include(gasto => gasto.Obra)
                .Include(gasto => gasto.TipoGasto)
                .ToList();
        }

        public Gasto? ObtenerPorId(int id)
        {
            return _context.Gastos
                .Include(gasto => gasto.Obra)
                .Include(gasto => gasto.TipoGasto)
                .FirstOrDefault(gasto => gasto.IdGasto == id);
        }

        public void Add(Gasto gasto)
        {
            gasto.Obra = null;
            gasto.TipoGasto = null;
            _context.Gastos.Add(gasto);
            _context.SaveChanges();
        }

        public Gasto? Update(int id, Gasto datos)
        {
            var gasto = _context.Gastos.Find(id);

            if (gasto == null)
            {
                return null;
            }

            gasto.DescGasto = datos.DescGasto;
            gasto.MontoGasto = datos.MontoGasto;
            gasto.ComprobanteGasto = datos.ComprobanteGasto;
            gasto.FechaGasto = datos.FechaGasto;
            gasto.IdObra = datos.IdObra;
            gasto.IdTipoGasto = datos.IdTipoGasto;

            _context.SaveChanges();
            return gasto;
        }

        public bool Delete(int id)
        {
            var gasto = _context.Gastos.Find(id);

            if (gasto == null)
            {
                return false;
            }

            _context.Gastos.Remove(gasto);
            _context.SaveChanges();
            return true;
        }
    }
}
