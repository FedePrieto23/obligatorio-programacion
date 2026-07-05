using Microsoft.EntityFrameworkCore;
using Obligatorio_Programacion.Data;
using Obligatorio_Programacion.Entity;

namespace Obligatorio_Programacion.Repository
{
    public class UsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Usuario> ObtenerTodos()
        {
            return _context.Usuarios
                .Include(usuario => usuario.Oficio)
                .ToList();
        }

        public Usuario? ObtenerPorId(int id)
        {
            return _context.Usuarios
                .Include(usuario => usuario.Oficio)
                .FirstOrDefault(usuario => usuario.IdUsuario == id);
        }

        public void Add(Usuario usuario)
        {
            usuario.Oficio = null;
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public Usuario? Update(int id, Usuario datos)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
            {
                return null;
            }

            usuario.NombreUsuario = datos.NombreUsuario;
            usuario.EmailUsuario = datos.EmailUsuario;
            usuario.Contraseña = datos.Contraseña;
            usuario.TipoEmpleado = datos.TipoEmpleado;
            usuario.IdOficio = datos.IdOficio;

            _context.SaveChanges();
            return usuario;
        }

        public bool Delete(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
            {
                return false;
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return true;
        }
    }
}
