using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VMedicals.Migrations
{
    public partial class updatedoctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractType",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "ContractValue",
                table: "Doctor");

            migrationBuilder.AddColumn<string>(
                name: "PhysicalConsultancyCharges",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhysicalConsultancyPercentage",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoConsultancyCharges",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoConsultancyPercentage",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhysicalConsultancyCharges",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "PhysicalConsultancyPercentage",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "VideoConsultancyCharges",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "VideoConsultancyPercentage",
                table: "Doctor");

            migrationBuilder.AddColumn<int>(
                name: "ContractType",
                table: "Doctor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ContractValue",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
