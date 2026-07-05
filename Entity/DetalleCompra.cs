namespace Obligatorio_Programacion.Entity
{
    public class DetalleCompra
    {
        public int IdDetalleCompra { get; set; }
        public int IdMaterial { get; set; }
        public int CantidadComprada { get; set; }
        public double PrecioUnitario { get; set; }
        public int IdCompra { get; set; }
        public Compra? Compra { get; set; }
        public Material? Material { get; set; }
    }
}
