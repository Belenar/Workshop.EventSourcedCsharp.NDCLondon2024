using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeerSender.Web.Migrations.Read_contextMigrations
{
    /// <inheritdoc />
    public partial class AddCheckpoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projection_checkpoints",
                columns: table => new
                {
                    Projection_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Event_version = table.Column<byte[]>(type: "binary(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projection_checkpoints", x => x.Projection_name);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projection_checkpoints");
        }
    }
}
