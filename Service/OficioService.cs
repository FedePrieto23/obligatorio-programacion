using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Repository;

namespace Obligatorio_Programacion.Service
{
    public class OficioService
    {
        private readonly OficioRepository _oficioRepository;

        public OficioService(OficioRepository oficioRepository)
        {
            _oficioRepository = oficioRepository;
        }

        public List<Oficio> ObtenerTodos()
        {
            return _oficioRepository.ObtenerTodos();
        }

        public Oficio? ObtenerPorId(int id)
        {
            return _oficioRepository.ObtenerPorId(id);
        }

        public void Add(Oficio oficio)
        {
            _oficioRepository.Add(oficio);
        }

        public Oficio? Update(int id, Oficio oficio)
        {
            return _oficioRepository.Update(id, oficio);
        }

        public bool Delete(int id)
        {
            return _oficioRepository.Delete(id);
        }
    }
}
