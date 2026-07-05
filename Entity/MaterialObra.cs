namespace Obligatorio_Programacion.Entity
{
    public class MaterialObra
    {
        public int IdMaterialObra { get; set; }
        public int IdObra { get; set; }
        public int CantidadMO { get; set; }
        public string EstadoMO { get; set; } = string.Empty;
        public int IdMaterial { get; set; }
        public Material? Material { get; set; }
        public Obra? Obra { get; set; }
    }
}
