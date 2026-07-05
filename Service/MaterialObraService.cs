using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Repository;

namespace Obligatorio_Programacion.Service
{
    public class MaterialObraService
    {
        private readonly MaterialObraRepository _materialObraRepository;

        public MaterialObraService(MaterialObraRepository materialObraRepository)
        {
            _materialObraRepository = materialObraRepository;
        }

        public List<MaterialObra> ObtenerTodos()
        {
            return _materialObraRepository.ObtenerTodos();
        }

        public MaterialObra? ObtenerPorId(int id)
        {
            return _materialObraRepository.ObtenerPorId(id);
        }

        public void Add(MaterialObra materialObra)
        {
            _materialObraRepository.Add(materialObra);
        }

        public MaterialObra? Update(int id, MaterialObra materialObra)
        {
            return _materialObraRepository.Update(id, materialObra);
        }

        public bool Delete(int id)
        {
            return _materialObraRepository.Delete(id);
        }
    }
}
