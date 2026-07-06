namespace Obligatorio_Programacion.Entity
{
    public class AvanceTarea
    {
        public int IdAvance { get; set; }
        public DateTime FechaAvance { get; set; }
        public decimal PorcentajeAvance { get; set; }
        public string DescripcionAvance { get; set; } = string.Empty;
        public int IdTarea { get; set; }
        public Tarea? Tarea { get; set; }
    }
}
