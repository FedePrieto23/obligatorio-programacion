using Obligatorio_Programacion.Entity;
using Obligatorio_Programacion.Data;

namespace Obligatorio_Programacion.Service
{
    public class AlertaService
    {
        private readonly AppDbContext _context;
        private const int CANTIDAD_MINIMA = 5; // Puedes hacerlo configurable

        public AlertaService(AppDbContext context)
        {
            _context = context;
        }

        public List<Alerta> ObtenerTodas()
        {
            return _context.Alertas.OrderByDescending(a => a.FechaAlerta).ToList();
        }

        public List<Alerta> ObtenerActivas()
        {
            return _context.Alertas
                .Where(a => !a.Resuelta)
                .OrderByDescending(a => a.FechaAlerta)
                .ToList();
        }

        public List<Alerta> ObtenerPorObra(int idObra)
        {
            return _context.Alertas
                .Where(a => a.IdObra == idObra && !a.Resuelta)
                .OrderByDescending(a => a.FechaAlerta)
                .ToList();
        }

        public void VerificarYGenerarAlertas()
        {
            var materialesObra = _context.MaterialesObras.ToList();

            foreach (var materialObra in materialesObra)
            {
                // Verificar si ya existe una alerta activa
                var alertaExistente = _context.Alertas
                    .Where(a => a.IdMaterialObra == materialObra.IdMaterialObra && !a.Resuelta)
                    .FirstOrDefault();

                if (materialObra.CantidadMO <= 0 && alertaExistente == null)
                {
                    // Generar alerta de material agotado
                    GenerarAlerta(materialObra, "Agotado", $"El material {materialObra.Material?.NombreMaterial} se ha agotado en la obra");
                }
                else if (materialObra.CantidadMO > 0 && materialObra.CantidadMO <= CANTIDAD_MINIMA && alertaExistente == null)
                {
                    // Generar alerta de stock bajo
                    GenerarAlerta(materialObra, "StockBajo", $"El material {materialObra.Material?.NombreMaterial} está bajo stock ({materialObra.CantidadMO} unidades)");
                }
                else if (materialObra.CantidadMO > CANTIDAD_MINIMA && alertaExistente != null && alertaExistente.TipoAlerta == "StockBajo")
                {
                    // Resolver alerta si el stock vuelve a niveles normales
                    alertaExistente.Resuelta = true;
                    alertaExistente.FechaResolucion = DateTime.Now;
                    _context.SaveChanges();
                }
            }
        }

        public void GenerarAlerta(MaterialObra materialObra, string tipoAlerta, string mensaje)
        {
            var alerta = new Alerta
            {
                IdMaterialObra = materialObra.IdMaterialObra,
                IdObra = materialObra.IdObra,
                TipoAlerta = tipoAlerta,
                Mensaje = mensaje,
                FechaAlerta = DateTime.Now,
                Resuelta = false,
                CantidadActual = materialObra.CantidadMO,
                CantidadMinima = CANTIDAD_MINIMA
            };

            _context.Alertas.Add(alerta);
            _context.SaveChanges();
        }

        public void ResolverAlerta(int idAlerta)
        {
            var alerta = _context.Alertas.Find(idAlerta);
            if (alerta != null)
            {
                alerta.Resuelta = true;
                alerta.FechaResolucion = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public int ObtenerCountAlertasActivas()
        {
            return _context.Alertas.Count(a => !a.Resuelta);
        }
    }
}
