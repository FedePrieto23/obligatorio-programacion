using Obligatorio_Programacion.Data;
using Obligatorio_Programacion.Entity;

namespace Obligatorio_Programacion.Repository
{
    public class TipoGastoRepository
    {
        private readonly AppDbContext _context;

        public TipoGastoRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<TipoGasto> ObtenerTodos()
        {
            return _context.TiposGasto.Distinct().ToList();
        }

        public TipoGasto? ObtenerPorId(int id)
        {
            return _context.TiposGasto.Find(id);
        }

        public void Add(TipoGasto tipoGasto)
        {
            _context.TiposGasto.Add(tipoGasto);
            _context.SaveChanges();
        }

        public TipoGasto? Update(int id, TipoGasto datos)
        {
            var tipoGasto = _context.TiposGasto.Find(id);

            if (tipoGasto == null)
            {
                return null;
            }

            tipoGasto.NombreTipoGasto = datos.NombreTipoGasto;

            _context.SaveChanges();
            return tipoGasto;
        }

        public bool Delete(int id)
        {
            var tipoGasto = _context.TiposGasto.Find(id);

            if (tipoGasto == null)
            {
                return false;
            }

            _context.TiposGasto.Remove(tipoGasto);
            _context.SaveChanges();
            return true;
        }
    }
}
