using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VMedicals.Migrations
{
    public partial class afterMaintainRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Appointment");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Patient",
                newName: "ModefiedBy");

            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "AspNetUsers",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Appointment",
                newName: "ModefiedBy");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Speciality",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Speciality",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModefiedBy",
                table: "Speciality",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Speciality",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Slots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Slots",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModefiedBy",
                table: "Slots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Slots",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PatientVitals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "PatientVitals",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModefiedBy",
                table: "PatientVitals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "PatientVitals",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PatientDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "PatientDocuments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModefiedBy",
                table: "PatientDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "PatientDocuments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Patient",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Patient",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DoctorDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "DoctorDocuments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModefiedBy",
                table: "DoctorDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "DoctorDocuments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Doctor",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModefiedBy",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Doctor",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Clinic",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Clinic",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModefiedBy",
                table: "Clinic",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Clinic",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Availabilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Availabilities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModefiedBy",
                table: "Availabilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Availabilities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModefiedBy",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AppointmentDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "AppointmentDocuments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModefiedBy",
                table: "AppointmentDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "AppointmentDocuments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Appointment",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Appointment",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Speciality");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Speciality");

            migrationBuilder.DropColumn(
                name: "ModefiedBy",
                table: "Speciality");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Speciality");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "ModefiedBy",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PatientVitals");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "PatientVitals");

            migrationBuilder.DropColumn(
                name: "ModefiedBy",
                table: "PatientVitals");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "PatientVitals");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PatientDocuments");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "PatientDocuments");

            migrationBuilder.DropColumn(
                name: "ModefiedBy",
                table: "PatientDocuments");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "PatientDocuments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DoctorDocuments");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "DoctorDocuments");

            migrationBuilder.DropColumn(
                name: "ModefiedBy",
                table: "DoctorDocuments");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "DoctorDocuments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "ModefiedBy",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "ModefiedBy",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "ModefiedBy",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ModefiedBy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AppointmentDocuments");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AppointmentDocuments");

            migrationBuilder.DropColumn(
                name: "ModefiedBy",
                table: "AppointmentDocuments");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "AppointmentDocuments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Appointment");

            migrationBuilder.RenameColumn(
                name: "ModefiedBy",
                table: "Patient",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "AspNetUsers",
                newName: "Updated");

            migrationBuilder.RenameColumn(
                name: "ModefiedBy",
                table: "Appointment",
                newName: "LastModifiedBy");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Patient",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Patient",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Appointment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Appointment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
