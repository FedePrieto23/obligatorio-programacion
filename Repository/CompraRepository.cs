using Microsoft.EntityFrameworkCore;
using Obligatorio_Programacion.Data;
using Obligatorio_Programacion.Entity;

namespace Obligatorio_Programacion.Repository
{
    public class CompraRepository
    {
        private readonly AppDbContext _context;

        public CompraRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Compra> ObtenerTodos()
        {
            return _context.Compras
                .Include(compra => compra.Obra)
                .Include(compra => compra.Proveedor)
                .ToList();
        }

        public Compra? ObtenerPorId(int id)
        {
            return _context.Compras
                .Include(compra => compra.Obra)
                .Include(compra => compra.Proveedor)
                .FirstOrDefault(compra => compra.IdCompra == id);
        }

        public void Add(Compra compra)
        {
            compra.Obra = null;
            compra.Proveedor = null;
            _context.Compras.Add(compra);
            _context.SaveChanges();
        }

        public Compra? Update(int id, Compra datos)
        {
            var compra = _context.Compras.Find(id);

            if (compra == null)
            {
                return null;
            }

            compra.FechaCompra = datos.FechaCompra;
            compra.EstadoCompra = datos.EstadoCompra;
            compra.ComprobanteCompra = datos.ComprobanteCompra;
            compra.ObservacionCompra = datos.ObservacionCompra;
            compra.IdProveedor = datos.IdProveedor;
            compra.IdObra = datos.IdObra;

            _context.SaveChanges();
            return compra;
        }

        public bool Delete(int id)
        {
            var compra = _context.Compras.Find(id);

            if (compra == null)
            {
                return false;
            }

            _context.Compras.Remove(compra);
            _context.SaveChanges();
            return true;
        }
    }
}
