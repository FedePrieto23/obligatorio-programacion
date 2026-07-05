namespace Obligatorio_Programacion.Entity
{
    public class Material
    {
        public int IdMaterial { get; set; }
        public string NombreMaterial { get; set; } = string.Empty;
        public string UnidadMedida { get; set; } = string.Empty;
        public double PrecioUnitario { get; set; }
        public string EstadoMaterial { get; set; } = string.Empty;
    }
}
