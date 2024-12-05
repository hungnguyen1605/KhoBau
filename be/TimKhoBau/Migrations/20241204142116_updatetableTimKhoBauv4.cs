using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimKhoBau.Migrations
{
    /// <inheritdoc />
    public partial class updatetableTimKhoBauv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_by",
                table: "kho_bau_entity");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "kho_bau_entity");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "kho_bau_entity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "kho_bau_entity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "kho_bau_entity",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "updated_by",
                table: "kho_bau_entity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
