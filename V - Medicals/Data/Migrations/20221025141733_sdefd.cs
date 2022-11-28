using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace V___Medicals.Data.Migrations
{
    public partial class sdefd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorDocument_Doctor_DoctorId",
                table: "DoctorDocument");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorDocument",
                table: "DoctorDocument");

            migrationBuilder.RenameTable(
                name: "DoctorDocument",
                newName: "DoctorDocuments");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorDocument_DoctorId",
                table: "DoctorDocuments",
                newName: "IX_DoctorDocuments_DoctorId");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorDocuments",
                table: "DoctorDocuments",
                column: "DoctorDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorDocuments_Doctor_DoctorId",
                table: "DoctorDocuments",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorDocuments_Doctor_DoctorId",
                table: "DoctorDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorDocuments",
                table: "DoctorDocuments");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Doctor");

            migrationBuilder.RenameTable(
                name: "DoctorDocuments",
                newName: "DoctorDocument");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorDocuments_DoctorId",
                table: "DoctorDocument",
                newName: "IX_DoctorDocument_DoctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorDocument",
                table: "DoctorDocument",
                column: "DoctorDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorDocument_Doctor_DoctorId",
                table: "DoctorDocument",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
