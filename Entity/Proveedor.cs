namespace Obligatorio_Programacion.Entity
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; } = string.Empty;
        public string RUT { get; set; } = string.Empty;
        public string TipoProveedor { get; set; } = string.Empty;
        public string EstadoProveedor { get; set; } = string.Empty;
        public string EmailProveedor { get; set; } = string.Empty;
        public string DireccionProveedor { get; set; } = string.Empty;
    }
}
