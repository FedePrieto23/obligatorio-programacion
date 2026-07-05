using Microsoft.EntityFrameworkCore;
using Obligatorio_Programacion.Data;
using Obligatorio_Programacion.Entity;

namespace Obligatorio_Programacion.Repository
{
    public class TareaRepository
    {
        private readonly AppDbContext _context;

        public TareaRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Tarea> ObtenerTodos()
        {
            return _context.Tareas
                .Include(tarea => tarea.Asignacion)
                .ToList();
        }

        public Tarea? ObtenerPorId(int id)
        {
            return _context.Tareas
                .Include(tarea => tarea.Asignacion)
                .FirstOrDefault(tarea => tarea.IdTarea == id);
        }

        public void Add(Tarea tarea)
        {
            tarea.Asignacion = null;
            _context.Tareas.Add(tarea);
            _context.SaveChanges();
        }

        public Tarea? Update(int id, Tarea datos)
        {
            var tarea = _context.Tareas.Find(id);

            if (tarea == null)
            {
                return null;
            }

            tarea.Prioridad = datos.Prioridad;
            tarea.DescTarea = datos.DescTarea;
            tarea.FechaInicio = datos.FechaInicio;
            tarea.FechaFin = datos.FechaFin;
            tarea.EstadoTarea = datos.EstadoTarea;
            tarea.IdAsignacion = datos.IdAsignacion;

            _context.SaveChanges();
            return tarea;
        }

        public bool Delete(int id)
        {
            var tarea = _context.Tareas.Find(id);

            if (tarea == null)
            {
                return false;
            }

            _context.Tareas.Remove(tarea);
            _context.SaveChanges();
            return true;
        }
    }
}
