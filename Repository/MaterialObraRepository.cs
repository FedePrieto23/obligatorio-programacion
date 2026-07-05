using Microsoft.EntityFrameworkCore;
using Obligatorio_Programacion.Data;
using Obligatorio_Programacion.Entity;

namespace Obligatorio_Programacion.Repository
{
    public class MaterialObraRepository
    {
        private readonly AppDbContext _context;

        public MaterialObraRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<MaterialObra> ObtenerTodos()
        {
            return _context.MaterialesObras
                .Include(materialObra => materialObra.Material)
                .Include(materialObra => materialObra.Obra)
                    .ThenInclude(obra => obra!.Cliente)
                .ToList();
        }

        public MaterialObra? ObtenerPorId(int id)
        {
            return _context.MaterialesObras
                .Include(materialObra => materialObra.Material)
                .Include(materialObra => materialObra.Obra)
                    .ThenInclude(obra => obra!.Cliente)
                .FirstOrDefault(materialObra => materialObra.IdMaterialObra == id);
        }

        public void Add(MaterialObra materialObra)
        {
            materialObra.Material = null;
            materialObra.Obra = null;
            _context.MaterialesObras.Add(materialObra);
            _context.SaveChanges();
        }

        public MaterialObra? Update(int id, MaterialObra datos)
        {
            var materialObra = _context.MaterialesObras.Find(id);

            if (materialObra == null)
            {
                return null;
            }

            materialObra.IdObra = datos.IdObra;
            materialObra.IdMaterial = datos.IdMaterial;
            materialObra.CantidadMO = datos.CantidadMO;
            materialObra.EstadoMO = datos.EstadoMO;

            _context.SaveChanges();
            return materialObra;
        }

        public bool Delete(int id)
        {
            var materialObra = _context.MaterialesObras.Find(id);

            if (materialObra == null)
            {
                return false;
            }

            _context.MaterialesObras.Remove(materialObra);
            _context.SaveChanges();
            return true;
        }
    }
}
