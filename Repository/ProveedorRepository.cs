using Obligatorio_Programacion.Data;
using Obligatorio_Programacion.Entity;

namespace Obligatorio_Programacion.Repository
{
    public class ProveedorRepository
    {
        private readonly AppDbContext _context;

        public ProveedorRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Proveedor> ObtenerTodos()
        {
            return _context.Proveedores.ToList();
        }

        public Proveedor? ObtenerPorId(int id)
        {
            return _context.Proveedores.Find(id);
        }

        public void Add(Proveedor proveedor)
        {
            _context.Proveedores.Add(proveedor);
            _context.SaveChanges();
        }

        public Proveedor? Update(int id, Proveedor datos)
        {
            var proveedor = _context.Proveedores.Find(id);

            if (proveedor == null)
            {
                return null;
            }

            proveedor.NombreProveedor = datos.NombreProveedor;
            proveedor.RUT = datos.RUT;
            proveedor.TipoProveedor = datos.TipoProveedor;
            proveedor.EstadoProveedor = datos.EstadoProveedor;
            proveedor.EmailProveedor = datos.EmailProveedor;
            proveedor.DireccionProveedor = datos.DireccionProveedor;

            _context.SaveChanges();
            return proveedor;
        }

        public bool Delete(int id)
        {
            var proveedor = _context.Proveedores.Find(id);

            if (proveedor == null)
            {
                return false;
            }

            _context.Proveedores.Remove(proveedor);
            _context.SaveChanges();
            return true;
        }
    }
}
