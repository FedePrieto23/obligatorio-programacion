using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Obligatorio_Programacion.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NombreCliente = table.Column<string>(type: "longtext", nullable: false),
                    ApellidoCliente = table.Column<string>(type: "longtext", nullable: false),
                    Documento = table.Column<string>(type: "longtext", nullable: false),
                    DireccionCliente = table.Column<string>(type: "longtext", nullable: false),
                    EmailCliente = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCliente);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Materiales",
                columns: table => new
                {
                    IdMaterial = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NombreMaterial = table.Column<string>(type: "longtext", nullable: false),
                    UnidadMedida = table.Column<string>(type: "longtext", nullable: false),
                    PrecioUnitario = table.Column<double>(type: "double", nullable: false),
                    EstadoMaterial = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiales", x => x.IdMaterial);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Oficios",
                columns: table => new
                {
                    IdOficio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NombreOficio = table.Column<string>(type: "longtext", nullable: false),
                    descripcionOficio = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oficios", x => x.IdOficio);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    IdProveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NombreProveedor = table.Column<string>(type: "longtext", nullable: false),
                    RUT = table.Column<string>(type: "longtext", nullable: false),
                    TipoProveedor = table.Column<string>(type: "longtext", nullable: false),
                    EstadoProveedor = table.Column<string>(type: "longtext", nullable: false),
                    EmailProveedor = table.Column<string>(type: "longtext", nullable: false),
                    DireccionProveedor = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.IdProveedor);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TiposGasto",
                columns: table => new
                {
                    IdTipoGasto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NombreTipoGasto = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposGasto", x => x.IdTipoGasto);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Obras",
                columns: table => new
                {
                    IdObra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NombreObra = table.Column<string>(type: "longtext", nullable: false),
                    Presupuesto = table.Column<double>(type: "double", nullable: false),
                    DireccionObra = table.Column<string>(type: "longtext", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EstadoObra = table.Column<string>(type: "longtext", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TelClientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    TelefonoCliente = table.Column<string>(type: "longtext", nullable: false)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NombreUsuario = table.Column<string>(type: "longtext", nullable: false),
                    EmailUsuario = table.Column<string>(type: "longtext", nullable: false),
                    Contraseña = table.Column<string>(type: "longtext", nullable: false),
                    TipoEmpleado = table.Column<string>(type: "longtext", nullable: false),
                    IdOficio = table.Column<int>(type: "int", nullable: false)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TelProveedores",
                columns: table => new
                {
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    TelefonoProveedor = table.Column<string>(type: "longtext", nullable: false)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    IdCompra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FechaCompra = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EstadoCompra = table.Column<string>(type: "longtext", nullable: false),
                    ComprobanteCompra = table.Column<string>(type: "longtext", nullable: false),
                    ObservacionCompra = table.Column<string>(type: "longtext", nullable: false),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    IdObra = table.Column<int>(type: "int", nullable: false)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Gastos",
                columns: table => new
                {
                    IdGasto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DescGasto = table.Column<string>(type: "longtext", nullable: false),
                    MontoGasto = table.Column<double>(type: "double", nullable: false),
                    ComprobanteGasto = table.Column<string>(type: "longtext", nullable: false),
                    FechaGasto = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IdObra = table.Column<int>(type: "int", nullable: false),
                    IdTipoGasto = table.Column<int>(type: "int", nullable: false)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MaterialesObras",
                columns: table => new
                {
                    IdMaterialObra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdObra = table.Column<int>(type: "int", nullable: false),
                    CantidadMO = table.Column<int>(type: "int", nullable: false),
                    EstadoMO = table.Column<string>(type: "longtext", nullable: false),
                    IdMaterial = table.Column<int>(type: "int", nullable: false)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Asignaciones",
                columns: table => new
                {
                    IdAsignacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdObra = table.Column<int>(type: "int", nullable: false)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RolesAdmin",
                columns: table => new
                {
                    IdUsuarioAd = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UsuarioIdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesAdmin", x => x.IdUsuarioAd);
                    table.ForeignKey(
                        name: "FK_RolesAdmin_Usuarios_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RolesEmpleado",
                columns: table => new
                {
                    IdUsuarioEmp = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UsuarioIdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesEmpleado", x => x.IdUsuarioEmp);
                    table.ForeignKey(
                        name: "FK_RolesEmpleado_Usuarios_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TelUsuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    TelefonoUsuario = table.Column<string>(type: "longtext", nullable: false)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DetallesCompras",
                columns: table => new
                {
                    IdDetalleCompra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdMaterial = table.Column<int>(type: "int", nullable: false),
                    CantidadComprada = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<double>(type: "double", nullable: false),
                    IdCompra = table.Column<int>(type: "int", nullable: false)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    IdTarea = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Prioridad = table.Column<string>(type: "longtext", nullable: false),
                    DescTarea = table.Column<string>(type: "longtext", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EstadoTarea = table.Column<string>(type: "longtext", nullable: false),
                    IdAsignacion = table.Column<int>(type: "int", nullable: false)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AvancesTareas",
                columns: table => new
                {
                    IdAvance = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FechaAvance = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PorcentajeAvance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DescripcionAvance = table.Column<string>(type: "longtext", nullable: false),
                    IdTarea = table.Column<int>(type: "int", nullable: false)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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
                name: "AvancesTareas");

            migrationBuilder.DropTable(
                name: "DetallesCompras");

            migrationBuilder.DropTable(
                name: "Gastos");

            migrationBuilder.DropTable(
                name: "MaterialesObras");

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
