namespace Obligatorio_Programacion.Entity
{
    public class TelProveedor
    {
        public int IdProveedor { get; set; }
        public string TelefonoProveedor { get; set; } = string.Empty;
        public Proveedor? Proveedor { get; set; }
    }
}
