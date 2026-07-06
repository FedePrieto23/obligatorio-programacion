namespace Obligatorio_Programacion.Entity
{
    public class Gasto
    {
        public int IdGasto { get; set; }
        public string DescGasto { get; set; } = string.Empty;
        public double MontoGasto { get; set; }
        public string ComprobanteGasto { get; set; } = string.Empty;
        public DateTime FechaGasto { get; set; }
        public int IdObra { get; set; }
        public int IdTipoGasto { get; set; }
        public Obra? Obra { get; set; }
        public TipoGasto? TipoGasto { get; set; }
    }
}
