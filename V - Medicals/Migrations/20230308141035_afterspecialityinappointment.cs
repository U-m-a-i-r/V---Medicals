using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VMedicals.Migrations
{
    public partial class afterspecialityinappointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedOn",
                table: "PatientVitals");

            migrationBuilder.AddColumn<string>(
                name: "SpecialityName",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecialityName",
                table: "Appointment");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "PatientVitals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
