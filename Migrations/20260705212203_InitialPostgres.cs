using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Obligatorio_Programacion.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auditorias",
                columns: table => new
                {
                    IdAuditoria = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    Accion = table.Column<string>(type: "text", nullable: false),
                    Tabla = table.Column<string>(type: "text", nullable: false),
                    IdRegistro = table.Column<int>(type: "integer", nullable: false),
                    DescripcionCambio = table.Column<string>(type: "text", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DatosAnteriores = table.Column<string>(type: "text", nullable: false),
                    DatosNuevos = table.Column<string>(type: "text", nullable: false),
                    DireccionIP = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditorias", x => x.IdAuditoria);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreCliente = table.Column<string>(type: "text", nullable: false),
                    ApellidoCliente = table.Column<string>(type: "text", nullable: false),
                    Documento = table.Column<string>(type: "text", nullable: false),
                    DireccionCliente = table.Column<string>(type: "text", nullable: false),
                    EmailCliente = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "Materiales",
                columns: table => new
                {
                    IdMaterial = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreMaterial = table.Column<string>(type: "text", nullable: false),
                    UnidadMedida = table.Column<string>(type: "text", nullable: false),
                    PrecioUnitario = table.Column<double>(type: "double precision", nullable: false),
                    EstadoMaterial = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiales", x => x.IdMaterial);
                });

            migrationBuilder.CreateTable(
                name: "Oficios",
                columns: table => new
                {
                    IdOficio = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreOficio = table.Column<string>(type: "text", nullable: false),
                    descripcionOficio = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oficios", x => x.IdOficio);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    IdProveedor = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreProveedor = table.Column<string>(type: "text", nullable: false),
                    RUT = table.Column<string>(type: "text", nullable: false),
                    TipoProveedor = table.Column<string>(type: "text", nullable: false),
                    EstadoProveedor = table.Column<string>(type: "text", nullable: false),
                    EmailProveedor = table.Column<string>(type: "text", nullable: false),
                    DireccionProveedor = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.IdProveedor);
                });

            migrationBuilder.CreateTable(
                name: "TiposGasto",
                columns: table => new
                {
                    IdTipoGasto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreTipoGasto = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposGasto", x => x.IdTipoGasto);
                });

            migrationBuilder.CreateTable(
                name: "Obras",
                columns: table => new
                {
                    IdObra = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreObra = table.Column<string>(type: "text", nullable: false),
                    Presupuesto = table.Column<double>(type: "double precision", nullable: false),
                    DireccionObra = table.Column<string>(type: "text", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EstadoObra = table.Column<string>(type: "text", nullable: false),
                    IdCliente = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obras", x => x.IdObra);
                    table.ForeignKey(
                        name: "FK_Obras_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelClientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "integer", nullable: false),
                    TelefonoCliente = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelClientes", x => x.IdCliente);
                    table.ForeignKey(
                        name: "FK_TelClientes_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreUsuario = table.Column<string>(type: "text", nullable: false),
                    EmailUsuario = table.Column<string>(type: "text", nullable: false),
                    Contraseña = table.Column<string>(type: "text", nullable: false),
                    TipoEmpleado = table.Column<string>(type: "text", nullable: false),
                    IdOficio = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Oficios_IdOficio",
                        column: x => x.IdOficio,
                        principalTable: "Oficios",
                        principalColumn: "IdOficio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelProveedores",
                columns: table => new
                {
                    IdProveedor = table.Column<int>(type: "integer", nullable: false),
                    TelefonoProveedor = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelProveedores", x => x.IdProveedor);
                    table.ForeignKey(
                        name: "FK_TelProveedores_Proveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "IdProveedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    IdCompra = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaCompra = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EstadoCompra = table.Column<string>(type: "text", nullable: false),
                    ComprobanteCompra = table.Column<string>(type: "text", nullable: false),
                    ObservacionCompra = table.Column<string>(type: "text", nullable: false),
                    IdProveedor = table.Column<int>(type: "integer", nullable: false),
                    IdObra = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.IdCompra);
                    table.ForeignKey(
                        name: "FK_Compras_Obras_IdObra",
                        column: x => x.IdObra,
                        principalTable: "Obras",
                        principalColumn: "IdObra",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Compras_Proveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "IdProveedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gastos",
                columns: table => new
                {
                    IdGasto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DescGasto = table.Column<string>(type: "text", nullable: false),
                    MontoGasto = table.Column<double>(type: "double precision", nullable: false),
                    ComprobanteGasto = table.Column<string>(type: "text", nullable: false),
                    FechaGasto = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdObra = table.Column<int>(type: "integer", nullable: false),
                    IdTipoGasto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gastos", x => x.IdGasto);
                    table.ForeignKey(
                        name: "FK_Gastos_Obras_IdObra",
                        column: x => x.IdObra,
                        principalTable: "Obras",
                        principalColumn: "IdObra",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gastos_TiposGasto_IdTipoGasto",
                        column: x => x.IdTipoGasto,
                        principalTable: "TiposGasto",
                        principalColumn: "IdTipoGasto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialesObras",
                columns: table => new
                {
                    IdMaterialObra = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdObra = table.Column<int>(type: "integer", nullable: false),
                    CantidadMO = table.Column<int>(type: "integer", nullable: false),
                    EstadoMO = table.Column<string>(type: "text", nullable: false),
                    IdMaterial = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialesObras", x => x.IdMaterialObra);
                    table.ForeignKey(
                        name: "FK_MaterialesObras_Materiales_IdMaterial",
                        column: x => x.IdMaterial,
                        principalTable: "Materiales",
                        principalColumn: "IdMaterial",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialesObras_Obras_IdObra",
                        column: x => x.IdObra,
                        principalTable: "Obras",
                        principalColumn: "IdObra",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Asignaciones",
                columns: table => new
                {
                    IdAsignacion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdObra = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignaciones", x => x.IdAsignacion);
                    table.ForeignKey(
                        name: "FK_Asignaciones_Obras_IdObra",
                        column: x => x.IdObra,
                        principalTable: "Obras",
                        principalColumn: "IdObra",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Asignaciones_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolesAdmin",
                columns: table => new
                {
                    IdUsuarioAd = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioIdUsuario = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesAdmin", x => x.IdUsuarioAd);
                    table.ForeignKey(
                        name: "FK_RolesAdmin_Usuarios_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "RolesEmpleado",
                columns: table => new
                {
                    IdUsuarioEmp = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioIdUsuario = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesEmpleado", x => x.IdUsuarioEmp);
                    table.ForeignKey(
                        name: "FK_RolesEmpleado_Usuarios_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "TelUsuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    TelefonoUsuario = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelUsuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_TelUsuarios_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallesCompras",
                columns: table => new
                {
                    IdDetalleCompra = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdMaterial = table.Column<int>(type: "integer", nullable: false),
                    CantidadComprada = table.Column<int>(type: "integer", nullable: false),
                    PrecioUnitario = table.Column<double>(type: "double precision", nullable: false),
                    IdCompra = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesCompras", x => x.IdDetalleCompra);
                    table.ForeignKey(
                        name: "FK_DetallesCompras_Compras_IdCompra",
                        column: x => x.IdCompra,
                        principalTable: "Compras",
                        principalColumn: "IdCompra",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesCompras_Materiales_IdMaterial",
                        column: x => x.IdMaterial,
                        principalTable: "Materiales",
                        principalColumn: "IdMaterial",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    IdAlerta = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdMaterialObra = table.Column<int>(type: "integer", nullable: false),
                    IdObra = table.Column<int>(type: "integer", nullable: false),
                    TipoAlerta = table.Column<string>(type: "text", nullable: false),
                    Mensaje = table.Column<string>(type: "text", nullable: false),
                    FechaAlerta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Resuelta = table.Column<bool>(type: "boolean", nullable: false),
                    FechaResolucion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CantidadActual = table.Column<int>(type: "integer", nullable: false),
                    CantidadMinima = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertas", x => x.IdAlerta);
                    table.ForeignKey(
                        name: "FK_Alertas_MaterialesObras_IdMaterialObra",
                        column: x => x.IdMaterialObra,
                        principalTable: "MaterialesObras",
                        principalColumn: "IdMaterialObra",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alertas_Obras_IdObra",
                        column: x => x.IdObra,
                        principalTable: "Obras",
                        principalColumn: "IdObra",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    IdTarea = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Prioridad = table.Column<string>(type: "text", nullable: false),
                    DescTarea = table.Column<string>(type: "text", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EstadoTarea = table.Column<string>(type: "text", nullable: false),
                    IdAsignacion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.IdTarea);
                    table.ForeignKey(
                        name: "FK_Tareas_Asignaciones_IdAsignacion",
                        column: x => x.IdAsignacion,
                        principalTable: "Asignaciones",
                        principalColumn: "IdAsignacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvancesTareas",
                columns: table => new
                {
                    IdAvance = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaAvance = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PorcentajeAvance = table.Column<decimal>(type: "numeric", nullable: false),
                    DescripcionAvance = table.Column<string>(type: "text", nullable: false),
                    IdTarea = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvancesTareas", x => x.IdAvance);
                    table.ForeignKey(
                        name: "FK_AvancesTareas_Tareas_IdTarea",
                        column: x => x.IdTarea,
                        principalTable: "Tareas",
                        principalColumn: "IdTarea",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_IdMaterialObra",
                table: "Alertas",
                column: "IdMaterialObra");

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_IdObra",
                table: "Alertas",
                column: "IdObra");

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_IdObra",
                table: "Asignaciones",
                column: "IdObra");

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_IdUsuario",
                table: "Asignaciones",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_AvancesTareas_IdTarea",
                table: "AvancesTareas",
                column: "IdTarea");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_IdObra",
                table: "Compras",
                column: "IdObra");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_IdProveedor",
                table: "Compras",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCompras_IdCompra",
                table: "DetallesCompras",
                column: "IdCompra");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCompras_IdMaterial",
                table: "DetallesCompras",
                column: "IdMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_IdObra",
                table: "Gastos",
                column: "IdObra");

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_IdTipoGasto",
                table: "Gastos",
                column: "IdTipoGasto");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialesObras_IdMaterial",
                table: "MaterialesObras",
                column: "IdMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialesObras_IdObra",
                table: "MaterialesObras",
                column: "IdObra");

            migrationBuilder.CreateIndex(
                name: "IX_Obras_IdCliente",
                table: "Obras",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_RolesAdmin_UsuarioIdUsuario",
                table: "RolesAdmin",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_RolesEmpleado_UsuarioIdUsuario",
                table: "RolesEmpleado",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_IdAsignacion",
                table: "Tareas",
                column: "IdAsignacion");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdOficio",
                table: "Usuarios",
                column: "IdOficio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropTable(
                name: "Auditorias");

            migrationBuilder.DropTable(
                name: "AvancesTareas");

            migrationBuilder.DropTable(
                name: "DetallesCompras");

            migrationBuilder.DropTable(
                name: "Gastos");

            migrationBuilder.DropTable(
                name: "RolesAdmin");

            migrationBuilder.DropTable(
                name: "RolesEmpleado");

            migrationBuilder.DropTable(
                name: "TelClientes");

            migrationBuilder.DropTable(
                name: "TelProveedores");

            migrationBuilder.DropTable(
                name: "TelUsuarios");

            migrationBuilder.DropTable(
                name: "MaterialesObras");

            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "TiposGasto");

            migrationBuilder.DropTable(
                name: "Materiales");

            migrationBuilder.DropTable(
                name: "Asignaciones");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Obras");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Oficios");
        }
    }
}
