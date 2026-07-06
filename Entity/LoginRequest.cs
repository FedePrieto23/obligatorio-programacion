namespace Obligatorio_Programacion.Entity
{
    public class LoginRequest
    {
        [System.Text.Json.Serialization.JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("contrasena")]
        public string Contrasena { get; set; } = string.Empty;
    }
}
