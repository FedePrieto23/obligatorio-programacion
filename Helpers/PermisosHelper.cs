using Microsoft.AspNetCore.Http;

namespace Obligatorio_Programacion.Helpers
{
    public static class PermisosHelper
    {
        public static bool EsAdministrador(HttpRequest request)
        {
            if (!request.Headers.TryGetValue("X-Rol-Usuario", out var rolHeader))
            {
                return false;
            }

            var rol = rolHeader.ToString();
            return rol.Equals("Administrador", StringComparison.OrdinalIgnoreCase);
        }

        public static bool EsEmpleadoOAdministrador(HttpRequest request)
        {
            if (!request.Headers.TryGetValue("X-Rol-Usuario", out var rolHeader))
            {
                return false;
            }

            var rol = rolHeader.ToString();
            return rol.Equals("Administrador", StringComparison.OrdinalIgnoreCase)
                || rol.Equals("Empleado", StringComparison.OrdinalIgnoreCase);
        }
    }
}
