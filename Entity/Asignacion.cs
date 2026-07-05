namespace Obligatorio_Programacion.Entity
{
    public class Asignacion
    {
        public int IdAsignacion { get; set; }
        public int IdUsuario { get; set; }
        public int IdObra { get; set; }
        public Obra? Obra { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
