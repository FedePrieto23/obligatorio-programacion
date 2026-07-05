using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Repository;

namespace Obligatorio_Programacion.Service
{
    public class MaterialService
    {
        private readonly MaterialRepository _materialRepository;

        public MaterialService(MaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public List<Material> ObtenerTodos()
        {
            return _materialRepository.ObtenerTodos();
        }

        public Material? ObtenerPorId(int id)
        {
            return _materialRepository.ObtenerPorId(id);
        }

        public void Add(Material material)
        {
            _materialRepository.Add(material);
        }

        public Material? Update(int id, Material material)
        {
            return _materialRepository.Update(id, material);
        }

        public bool Delete(int id)
        {
            return _materialRepository.Delete(id);
        }
    }
}
