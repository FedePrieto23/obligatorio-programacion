using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Obligatorio_Programacion.Migrations
{
    /// <inheritdoc />
    public partial class AgregarAuditoriaYAlertas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolesAdmin_Usuarios_UsuarioIdUsuario",
                table: "RolesAdmin");

            migrationBuilder.DropForeignKey(
                name: "FK_RolesEmpleado_Usuarios_UsuarioIdUsuario",
                table: "RolesEmpleado");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioIdUsuario",
                table: "RolesEmpleado",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioIdUsuario",
                table: "RolesAdmin",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    IdAlerta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdMaterialObra = table.Column<int>(type: "int", nullable: false),
                    IdObra = table.Column<int>(type: "int", nullable: false),
                    TipoAlerta = table.Column<string>(type: "longtext", nullable: false),
                    Mensaje = table.Column<string>(type: "longtext", nullable: false),
                    FechaAlerta = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Resuelta = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FechaResolucion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CantidadActual = table.Column<int>(type: "int", nullable: false),
                    CantidadMinima = table.Column<int>(type: "int", nullable: false)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Auditorias",
                columns: table => new
                {
                    IdAuditoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Accion = table.Column<string>(type: "longtext", nullable: false),
                    Tabla = table.Column<string>(type: "longtext", nullable: false),
                    IdRegistro = table.Column<int>(type: "int", nullable: false),
                    DescripcionCambio = table.Column<string>(type: "longtext", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DatosAnteriores = table.Column<string>(type: "longtext", nullable: false),
                    DatosNuevos = table.Column<string>(type: "longtext", nullable: false),
                    DireccionIP = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditorias", x => x.IdAuditoria);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_IdMaterialObra",
                table: "Alertas",
                column: "IdMaterialObra");

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_IdObra",
                table: "Alertas",
                column: "IdObra");

            migrationBuilder.AddForeignKey(
                name: "FK_RolesAdmin_Usuarios_UsuarioIdUsuario",
                table: "RolesAdmin",
                column: "UsuarioIdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_RolesEmpleado_Usuarios_UsuarioIdUsuario",
                table: "RolesEmpleado",
                column: "UsuarioIdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolesAdmin_Usuarios_UsuarioIdUsuario",
                table: "RolesAdmin");

            migrationBuilder.DropForeignKey(
                name: "FK_RolesEmpleado_Usuarios_UsuarioIdUsuario",
                table: "RolesEmpleado");

            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropTable(
                name: "Auditorias");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioIdUsuario",
                table: "RolesEmpleado",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioIdUsuario",
                table: "RolesAdmin",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RolesAdmin_Usuarios_UsuarioIdUsuario",
                table: "RolesAdmin",
                column: "UsuarioIdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolesEmpleado_Usuarios_UsuarioIdUsuario",
                table: "RolesEmpleado",
                column: "UsuarioIdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
