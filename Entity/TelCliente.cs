namespace Obligatorio_Programacion.Entity
{
    public class TelCliente
    {
        public int IdCliente { get; set; }
        public string TelefonoCliente { get; set; } = string.Empty;
        public Cliente? Cliente { get; set; }
    }
}
