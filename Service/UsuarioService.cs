using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Repository;
using BCrypt.Net;

namespace Obligatorio_Programacion.Service
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioService(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public List<Usuario> ObtenerTodos()
        {
            return _usuarioRepository.ObtenerTodos();
        }

        public Usuario? ObtenerPorId(int id)
        {
            return _usuarioRepository.ObtenerPorId(id);
        }

        public void Add(Usuario usuario)
        {
            usuario.Contraseña = HashPassword(usuario.Contraseña);
            _usuarioRepository.Add(usuario);
        }

        public Usuario? Update(int id, Usuario usuario)
        {
            usuario.Contraseña = HashPassword(usuario.Contraseña);
            return _usuarioRepository.Update(id, usuario);
        }

        public bool Delete(int id)
        {
            return _usuarioRepository.Delete(id);
        }

        public bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
            }
            catch
            {
                return false;
            }
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }
    }
}
