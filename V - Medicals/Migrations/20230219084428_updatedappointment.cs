using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VMedicals.Migrations
{
    public partial class updatedappointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminNotes",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorNotes",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientNotes",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminNotes",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "DoctorNotes",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "PatientNotes",
                table: "Appointment");
        }
    }
}
