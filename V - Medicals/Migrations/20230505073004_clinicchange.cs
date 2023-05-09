using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VMedicals.Migrations
{
    public partial class clinicchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Clinic",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Clinic",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Clinic");
        }
    }
}
