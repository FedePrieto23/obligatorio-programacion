using Microsoft.EntityFrameworkCore;
using Obligatorio_Programacion.Entity;

namespace Obligatorio_Programacion.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Asignacion> Asignaciones { get; set; }
        public DbSet<AvanceTarea> AvancesTareas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<DetalleCompra> DetallesCompras { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<MaterialObra> MaterialesObras { get; set; }
        public DbSet<Obra> Obras { get; set; }
        public DbSet<Oficio> Oficios { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<RolAdmin> RolesAdmin { get; set; }
        public DbSet<RolEmpleado> RolesEmpleado { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<TelCliente> TelClientes { get; set; }
        public DbSet<TelProveedor> TelProveedores { get; set; }
        public DbSet<TelUsuario> TelUsuarios { get; set; }
        public DbSet<TipoGasto> TiposGasto { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }
        public DbSet<Alerta> Alertas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Asignacion>().HasKey(asignacion => asignacion.IdAsignacion);
            modelBuilder.Entity<AvanceTarea>().HasKey(avanceTarea => avanceTarea.IdAvance);
            modelBuilder.Entity<Cliente>().HasKey(cliente => cliente.IdCliente);
            modelBuilder.Entity<Compra>().HasKey(compra => compra.IdCompra);
            modelBuilder.Entity<DetalleCompra>().HasKey(detalleCompra => detalleCompra.IdDetalleCompra);
            modelBuilder.Entity<Gasto>().HasKey(gasto => gasto.IdGasto);
            modelBuilder.Entity<Material>().HasKey(material => material.IdMaterial);
            modelBuilder.Entity<MaterialObra>().HasKey(materialObra => materialObra.IdMaterialObra);
            modelBuilder.Entity<Obra>().HasKey(obra => obra.IdObra);
            modelBuilder.Entity<Oficio>().HasKey(oficio => oficio.IdOficio);
            modelBuilder.Entity<Proveedor>().HasKey(proveedor => proveedor.IdProveedor);
            modelBuilder.Entity<RolAdmin>().HasKey(rolAdmin => rolAdmin.IdUsuarioAd);
            modelBuilder.Entity<RolEmpleado>().HasKey(rolEmpleado => rolEmpleado.IdUsuarioEmp);
            modelBuilder.Entity<Tarea>().HasKey(tarea => tarea.IdTarea);
            modelBuilder.Entity<TelCliente>().HasKey(telCliente => telCliente.IdCliente);
            modelBuilder.Entity<TelProveedor>().HasKey(telProveedor => telProveedor.IdProveedor);
            modelBuilder.Entity<TelUsuario>().HasKey(telUsuario => telUsuario.IdUsuario);
            modelBuilder.Entity<TipoGasto>().HasKey(tipoGasto => tipoGasto.IdTipoGasto);
            modelBuilder.Entity<Usuario>().HasKey(usuario => usuario.IdUsuario);
            modelBuilder.Entity<Auditoria>().HasKey(auditoria => auditoria.IdAuditoria);
            modelBuilder.Entity<Alerta>().HasKey(alerta => alerta.IdAlerta);

            modelBuilder.Entity<Obra>()
                .HasOne(obra => obra.Cliente)
                .WithMany()
                .HasForeignKey(obra => obra.IdCliente);

            modelBuilder.Entity<Compra>()
                .HasOne(compra => compra.Proveedor)
                .WithMany()
                .HasForeignKey(compra => compra.IdProveedor);

            modelBuilder.Entity<Compra>()
                .HasOne(compra => compra.Obra)
                .WithMany()
                .HasForeignKey(compra => compra.IdObra);

            modelBuilder.Entity<Gasto>()
                .HasOne(gasto => gasto.Obra)
                .WithMany()
                .HasForeignKey(gasto => gasto.IdObra);

            modelBuilder.Entity<Gasto>()
                .HasOne(gasto => gasto.TipoGasto)
                .WithMany()
                .HasForeignKey(gasto => gasto.IdTipoGasto);

            modelBuilder.Entity<Tarea>()
                .HasOne(tarea => tarea.Asignacion)
                .WithMany()
                .HasForeignKey(tarea => tarea.IdAsignacion);

            modelBuilder.Entity<Usuario>()
                .HasOne(usuario => usuario.Oficio)
                .WithMany()
                .HasForeignKey(usuario => usuario.IdOficio);

            modelBuilder.Entity<Asignacion>()
                .HasOne(a => a.Obra)
                .WithMany()
                .HasForeignKey(a => a.IdObra);

            modelBuilder.Entity<Asignacion>()
                .HasOne(a => a.Usuario)
                .WithMany()
                .HasForeignKey(a => a.IdUsuario);

            modelBuilder.Entity<AvanceTarea>()
                .HasOne(a => a.Tarea)
                .WithMany()
                .HasForeignKey(a => a.IdTarea);

            modelBuilder.Entity<DetalleCompra>()
                .HasOne(d => d.Compra)
                .WithMany()
                .HasForeignKey(d => d.IdCompra);

            modelBuilder.Entity<DetalleCompra>()
                .HasOne(d => d.Material)
                .WithMany()
                .HasForeignKey(d => d.IdMaterial);

            modelBuilder.Entity<MaterialObra>()
                .HasOne(mo => mo.Obra)
                .WithMany()
                .HasForeignKey(mo => mo.IdObra);

            modelBuilder.Entity<MaterialObra>()
                .HasOne(mo => mo.Material)
                .WithMany()
                .HasForeignKey(mo => mo.IdMaterial);

            modelBuilder.Entity<TelCliente>()
                .HasOne(t => t.Cliente)
                .WithMany()
                .HasForeignKey(t => t.IdCliente);

            modelBuilder.Entity<TelProveedor>()
                .HasOne(t => t.Proveedor)
                .WithMany()
                .HasForeignKey(t => t.IdProveedor);

            modelBuilder.Entity<TelUsuario>()
                .HasOne(t => t.Usuario)
                .WithMany()
                .HasForeignKey(t => t.IdUsuario);

            modelBuilder.Entity<Alerta>()
                .HasOne(a => a.MaterialObra)
                .WithMany()
                .HasForeignKey(a => a.IdMaterialObra);

            modelBuilder.Entity<Alerta>()
                .HasOne(a => a.Obra)
                .WithMany()
                .HasForeignKey(a => a.IdObra);
        }
    }
}
