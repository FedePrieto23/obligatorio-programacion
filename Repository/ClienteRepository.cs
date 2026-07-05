using Obligatorio_Programacion.Data;
using Obligatorio_Programacion.Entity;

namespace Obligatorio_Programacion.Repository
{
    public class ClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Cliente> ObtenerTodos()
        {
            return _context.Clientes.ToList();
        }

        public Cliente? ObtenerPorId(int id)
        {
            return _context.Clientes.Find(id);
        }

        public void Add(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }

        public Cliente? Update(int id, Cliente datos)
        {
            var cliente = _context.Clientes.Find(id);

            if (cliente == null)
            {
                return null;
            }

            cliente.NombreCliente = datos.NombreCliente;
            cliente.ApellidoCliente = datos.ApellidoCliente;
            cliente.Documento = datos.Documento;
            cliente.DireccionCliente = datos.DireccionCliente;
            cliente.EmailCliente = datos.EmailCliente;

            _context.SaveChanges();
            return cliente;
        }

        public bool Delete(int id)
        {
            var cliente = _context.Clientes.Find(id);

            if (cliente == null)
            {
                return false;
            }

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
            return true;
        }
    }
}
