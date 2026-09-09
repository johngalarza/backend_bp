using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddClientesReadModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clientes_read_model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Identificacion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes_read_model", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_clientes_read_model_ClienteId",
                table: "clientes_read_model",
                column: "ClienteId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clientes_read_model");
        }
    }
}
