namespace Obligatorio_Programacion.Entity
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public DateTime FechaCompra { get; set; }
        public string EstadoCompra { get; set; } = string.Empty;
        public string ComprobanteCompra { get; set; } = string.Empty;
        public string ObservacionCompra { get; set; } = string.Empty;
        public int IdProveedor { get; set; }
        public int IdObra { get; set; }
        public Proveedor? Proveedor { get; set; }
        public Obra? Obra { get; set; }
    }
}
