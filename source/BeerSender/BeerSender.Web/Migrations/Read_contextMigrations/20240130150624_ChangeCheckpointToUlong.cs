using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeerSender.Web.Migrations.Read_contextMigrations
{
    /// <inheritdoc />
    public partial class ChangeCheckpointToUlong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Event_version",
                table: "Projection_checkpoints",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(8)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Event_version",
                table: "Projection_checkpoints",
                type: "binary(8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");
        }
    }
}
