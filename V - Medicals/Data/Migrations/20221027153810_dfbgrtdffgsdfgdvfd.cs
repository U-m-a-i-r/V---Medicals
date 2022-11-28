using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace V___Medicals.Data.Migrations
{
    public partial class dfbgrtdffgsdfgdvfd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Doctor",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Doctor");
        }
    }
}
