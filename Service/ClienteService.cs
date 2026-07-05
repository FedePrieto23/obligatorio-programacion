using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Repository;

namespace Obligatorio_Programacion.Service
{
    public class ClienteService
    {
        private readonly ClienteRepository _clienteRepository;

        public ClienteService(ClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public List<Cliente> ObtenerTodos()
        {
            return _clienteRepository.ObtenerTodos();
        }

        public Cliente? ObtenerPorId(int id)
        {
            return _clienteRepository.ObtenerPorId(id);
        }

        public void Add(Cliente cliente)
        {
            _clienteRepository.Add(cliente);
        }

        public Cliente? Update(int id, Cliente cliente)
        {
            return _clienteRepository.Update(id, cliente);
        }

        public bool Delete(int id)
        {
            return _clienteRepository.Delete(id);
        }
    }
}
