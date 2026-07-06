namespace Obligatorio_Programacion.Entity
{
    public class Auditoria
    {
        public int IdAuditoria { get; set; }
        public int IdUsuario { get; set; }
        public string Accion { get; set; } = string.Empty;
        public string Tabla { get; set; } = string.Empty;
        public int IdRegistro { get; set; }
        public string DescripcionCambio { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public string DatosAnteriores { get; set; } = string.Empty;
        public string DatosNuevos { get; set; } = string.Empty;
        public string DireccionIP { get; set; } = string.Empty;
    }
}
