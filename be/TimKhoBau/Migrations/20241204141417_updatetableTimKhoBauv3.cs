using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimKhoBau.Migrations
{
    /// <inheritdoc />
    public partial class updatetableTimKhoBauv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "matrix",
                table: "kho_bau_entity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "total_fuel",
                table: "kho_bau_entity",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "matrix",
                table: "kho_bau_entity");

            migrationBuilder.DropColumn(
                name: "total_fuel",
                table: "kho_bau_entity");
        }
    }
}
