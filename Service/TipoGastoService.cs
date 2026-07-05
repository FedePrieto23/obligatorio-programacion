using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Repository;

namespace Obligatorio_Programacion.Service
{
    public class TipoGastoService
    {
        private readonly TipoGastoRepository _tipoGastoRepository;

        public TipoGastoService(TipoGastoRepository tipoGastoRepository)
        {
            _tipoGastoRepository = tipoGastoRepository;
        }

        public List<TipoGasto> ObtenerTodos()
        {
            return _tipoGastoRepository.ObtenerTodos();
        }

        public TipoGasto? ObtenerPorId(int id)
        {
            return _tipoGastoRepository.ObtenerPorId(id);
        }

        public void Add(TipoGasto tipoGasto)
        {
            _tipoGastoRepository.Add(tipoGasto);
        }

        public TipoGasto? Update(int id, TipoGasto tipoGasto)
        {
            return _tipoGastoRepository.Update(id, tipoGasto);
        }

        public bool Delete(int id)
        {
            return _tipoGastoRepository.Delete(id);
        }
    }
}
