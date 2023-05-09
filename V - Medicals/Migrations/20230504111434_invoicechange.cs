using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VMedicals.Migrations
{
    public partial class invoicechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PatientVitals_AppointmentId",
                table: "PatientVitals");

            migrationBuilder.AddColumn<string>(
                name: "payproInvoiceURL",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientVitals_AppointmentId",
                table: "PatientVitals",
                column: "AppointmentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PatientVitals_AppointmentId",
                table: "PatientVitals");

            migrationBuilder.DropColumn(
                name: "payproInvoiceURL",
                table: "Invoices");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVitals_AppointmentId",
                table: "PatientVitals",
                column: "AppointmentId");
        }
    }
}
