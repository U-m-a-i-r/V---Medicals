using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace V___Medicals.Data.Migrations
{
    public partial class dfbgrtdffgsdfgdvfdfg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "Doctor",
                newName: "Discription");

            migrationBuilder.AddColumn<string>(
                name: "Qualification",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "Doctor");

            migrationBuilder.RenameColumn(
                name: "Discription",
                table: "Doctor",
                newName: "Summary");
        }
    }
}
