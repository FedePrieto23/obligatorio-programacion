using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Repository;

namespace Obligatorio_Programacion.Service
{
    public class ObraService
    {
        private readonly ObraRepository _obraRepository;

        public ObraService(ObraRepository obraRepository)
        {
            _obraRepository = obraRepository;
        }

        public List<Obra> ObtenerTodos()
        {
            return _obraRepository.ObtenerTodos();
        }

        public Obra? ObtenerPorId(int id)
        {
            return _obraRepository.ObtenerPorId(id);
        }

        public void Add(Obra obra)
        {
            _obraRepository.Add(obra);
        }

        public Obra? Update(int id, Obra obra)
        {
            return _obraRepository.Update(id, obra);
        }

        public bool Delete(int id)
        {
            return _obraRepository.Delete(id);
        }
    }
}
