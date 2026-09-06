using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Genero = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Edad = table.Column<int>(type: "integer", nullable: false),
                    Identificacion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Direccion = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_clientes_ClienteId",
                table: "clientes",
                column: "ClienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clientes_Identificacion",
                table: "clientes",
                column: "Identificacion",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clientes");
        }
    }
}
