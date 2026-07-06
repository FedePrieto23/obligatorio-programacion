using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Repository;

namespace Obligatorio_Programacion.Service
{
    public class ProveedorService
    {
        private readonly ProveedorRepository _proveedorRepository;

        public ProveedorService(ProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        public List<Proveedor> ObtenerTodos()
        {
            return _proveedorRepository.ObtenerTodos();
        }

        public Proveedor? ObtenerPorId(int id)
        {
            return _proveedorRepository.ObtenerPorId(id);
        }

        public void Add(Proveedor proveedor)
        {
            _proveedorRepository.Add(proveedor);
        }

        public Proveedor? Update(int id, Proveedor proveedor)
        {
            return _proveedorRepository.Update(id, proveedor);
        }

        public bool Delete(int id)
        {
            return _proveedorRepository.Delete(id);
        }
    }
}
