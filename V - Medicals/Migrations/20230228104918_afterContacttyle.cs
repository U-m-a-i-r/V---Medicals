using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VMedicals.Migrations
{
    public partial class afterContacttyle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractType",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "ContractValue",
                table: "Doctor");
        }
    }
}
