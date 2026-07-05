namespace Obligatorio_Programacion.Entity
{
    public class Obra
    {
        public int IdObra { get; set; }
        public string NombreObra { get; set; } = string.Empty;
        public double Presupuesto { get; set; }
        public string DireccionObra { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string EstadoObra { get; set; } = string.Empty;
        public int IdCliente { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
