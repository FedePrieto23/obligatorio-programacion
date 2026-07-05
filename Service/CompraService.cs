using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Repository;

namespace Obligatorio_Programacion.Service
{
    public class CompraService
    {
        private readonly CompraRepository _compraRepository;

        public CompraService(CompraRepository compraRepository)
        {
            _compraRepository = compraRepository;
        }

        public List<Compra> ObtenerTodos()
        {
            return _compraRepository.ObtenerTodos();
        }

        public Compra? ObtenerPorId(int id)
        {
            return _compraRepository.ObtenerPorId(id);
        }

        public void Add(Compra compra)
        {
            _compraRepository.Add(compra);
        }

        public Compra? Update(int id, Compra compra)
        {
            return _compraRepository.Update(id, compra);
        }

        public bool Delete(int id)
        {
            return _compraRepository.Delete(id);
        }
    }
}
