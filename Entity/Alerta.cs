namespace Obligatorio_Programacion.Entity
{
    public class Alerta
    {
        public int IdAlerta { get; set; }
        public int IdMaterialObra { get; set; }
        public int IdObra { get; set; }
        public string TipoAlerta { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public DateTime FechaAlerta { get; set; }
        public bool Resuelta { get; set; } = false;
        public DateTime? FechaResolucion { get; set; }
        public int CantidadActual { get; set; }
        public int CantidadMinima { get; set; }

        public MaterialObra? MaterialObra { get; set; }
        public Obra? Obra { get; set; }
    }
}
