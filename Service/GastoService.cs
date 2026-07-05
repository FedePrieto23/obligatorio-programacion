using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Repository;

namespace Obligatorio_Programacion.Service
{
    public class GastoService
    {
        private readonly GastoRepository _gastoRepository;

        public GastoService(GastoRepository gastoRepository)
        {
            _gastoRepository = gastoRepository;
        }

        public List<Gasto> ObtenerTodos()
        {
            return _gastoRepository.ObtenerTodos();
        }

        public Gasto? ObtenerPorId(int id)
        {
            return _gastoRepository.ObtenerPorId(id);
        }

        public void Add(Gasto gasto)
        {
            _gastoRepository.Add(gasto);
        }

        public Gasto? Update(int id, Gasto gasto)
        {
            return _gastoRepository.Update(id, gasto);
        }

        public bool Delete(int id)
        {
            return _gastoRepository.Delete(id);
        }
    }
}
