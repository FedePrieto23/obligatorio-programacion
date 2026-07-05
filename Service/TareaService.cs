using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Repository;

namespace Obligatorio_Programacion.Service
{
    public class TareaService
    {
        private readonly TareaRepository _tareaRepository;

        public TareaService(TareaRepository tareaRepository)
        {
            _tareaRepository = tareaRepository;
        }

        public List<Tarea> ObtenerTodos()
        {
            return _tareaRepository.ObtenerTodos();
        }

        public Tarea? ObtenerPorId(int id)
        {
            return _tareaRepository.ObtenerPorId(id);
        }

        public void Add(Tarea tarea)
        {
            _tareaRepository.Add(tarea);
        }

        public Tarea? Update(int id, Tarea tarea)
        {
            return _tareaRepository.Update(id, tarea);
        }

        public bool Delete(int id)
        {
            return _tareaRepository.Delete(id);
        }
    }
}
