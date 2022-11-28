using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace V___Medicals.Data.Migrations
{
    public partial class dfbgrtdffgsdfgdvf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentPath",
                table: "DoctorDocuments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentPath",
                table: "DoctorDocuments");
        }
    }
}
