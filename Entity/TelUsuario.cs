namespace Obligatorio_Programacion.Entity
{
    public class TelUsuario
    {
        public int IdUsuario { get; set; }
        public string TelefonoUsuario { get; set; } = string.Empty;
        public Usuario? Usuario { get; set; }
    }
}
