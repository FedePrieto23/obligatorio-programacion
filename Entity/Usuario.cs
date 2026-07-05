namespace Obligatorio_Programacion.Entity
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string EmailUsuario { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;
        public string TipoEmpleado { get; set; } = string.Empty;
        public int IdOficio { get; set; }
        public Oficio? Oficio { get; set; }
    }
}
