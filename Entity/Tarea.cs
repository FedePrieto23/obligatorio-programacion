namespace Obligatorio_Programacion.Entity
{
    public class Tarea
    {
        public int IdTarea { get; set; }
        public string Prioridad { get; set; } = string.Empty;
        public string DescTarea { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string EstadoTarea { get; set; } = string.Empty;
        public int IdAsignacion { get; set; }
        public Asignacion? Asignacion { get; set; }
    }
}
