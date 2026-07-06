using Obligatorio_Programacion.Data;
using Obligatorio_Programacion.Entity;

namespace Obligatorio_Programacion.Repository
{
    public class MaterialRepository
    {
        private readonly AppDbContext _context;

        public MaterialRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Material> ObtenerTodos()
        {
            return _context.Materiales.ToList();
        }

        public Material? ObtenerPorId(int id)
        {
            return _context.Materiales.Find(id);
        }

        public void Add(Material material)
        {
            _context.Materiales.Add(material);
            _context.SaveChanges();
        }

        public Material? Update(int id, Material datos)
        {
            var material = _context.Materiales.Find(id);

            if (material == null)
            {
                return null;
            }

            material.NombreMaterial = datos.NombreMaterial;
            material.UnidadMedida = datos.UnidadMedida;
            material.PrecioUnitario = datos.PrecioUnitario;
            material.EstadoMaterial = datos.EstadoMaterial;

            _context.SaveChanges();
            return material;
        }

        public bool Delete(int id)
        {
            var material = _context.Materiales.Find(id);

            if (material == null)
            {
                return false;
            }

            _context.Materiales.Remove(material);
            _context.SaveChanges();
            return true;
        }
    }
}
