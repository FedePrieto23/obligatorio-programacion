using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Repository;

namespace Obligatorio_Programacion.Service
{
    public class AsignacionService
    {
        private readonly AsignacionRepository _asignacionRepository;

        public AsignacionService(AsignacionRepository asignacionRepository)
        {
            _asignacionRepository = asignacionRepository;
        }

        public List<Asignacion> ObtenerTodos()
        {
            return _asignacionRepository.ObtenerTodos();
        }

        public Asignacion? ObtenerPorId(int id)
        {
            return _asignacionRepository.ObtenerPorId(id);
        }

        public Asignacion? ObtenerPorUsuarioYObra(int idUsuario, int idObra)
        {
            return _asignacionRepository.ObtenerPorUsuarioYObra(idUsuario, idObra);
        }

        public void Add(Asignacion asignacion)
        {
            _asignacionRepository.Add(asignacion);
        }

        public Asignacion? Update(int id, Asignacion asignacion)
        {
            return _asignacionRepository.Update(id, asignacion);
        }

        public bool Delete(int id)
        {
            return _asignacionRepository.Delete(id);
        }
    }
}
