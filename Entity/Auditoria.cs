namespace Obligatorio_Programacion.Entity
{
    public class Auditoria
    {
        public int IdAuditoria { get; set; }
        public int IdUsuario { get; set; }
        public string Accion { get; set; } = string.Empty; // Create, Update, Delete
        public string Tabla { get; set; } = string.Empty; // Obra, Usuario, Material, etc.
        public int IdRegistro { get; set; } // ID del registro afectado
        public string DescripcionCambio { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public string DatosAnteriores { get; set; } = string.Empty; // JSON
        public string DatosNuevos { get; set; } = string.Empty; // JSON
        public string DireccionIP { get; set; } = string.Empty;
    }
}
