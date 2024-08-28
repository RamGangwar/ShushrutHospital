using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShushrutEyeHospitalCRM.Migrations
{
    public partial class revertchnagesofcounslingtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalProcedure",
                table: "Counsling");

            migrationBuilder.DropColumn(
                name: "AnesthesiaType",
                table: "Counsling");

            migrationBuilder.DropColumn(
                name: "ApproxCharge",
                table: "Counsling");

            migrationBuilder.DropColumn(
                name: "BookingDate",
                table: "Counsling");

            migrationBuilder.DropColumn(
                name: "Diagnosis",
                table: "Counsling");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Counsling");

            migrationBuilder.DropColumn(
                name: "MRNO",
                table: "Counsling");

            migrationBuilder.DropColumn(
                name: "OperatedEye",
                table: "Counsling");

            migrationBuilder.DropColumn(
                name: "PackageName",
                table: "Counsling");

            migrationBuilder.RenameColumn(
                name: "SurgeryDateTime",
                table: "Counsling",
                newName: "DOB");

            migrationBuilder.RenameColumn(
                name: "Remarks",
                table: "Counsling",
                newName: "ProfilePic");

            migrationBuilder.RenameColumn(
                name: "ProcedureName",
                table: "Counsling",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "PatientOrParty",
                table: "Counsling",
                newName: "Address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePic",
                table: "Counsling",
                newName: "Remarks");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Counsling",
                newName: "ProcedureName");

            migrationBuilder.RenameColumn(
                name: "DOB",
                table: "Counsling",
                newName: "SurgeryDateTime");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Counsling",
                newName: "PatientOrParty");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalProcedure",
                table: "Counsling",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnesthesiaType",
                table: "Counsling",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ApproxCharge",
                table: "Counsling",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "BookingDate",
                table: "Counsling",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Diagnosis",
                table: "Counsling",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Counsling",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MRNO",
                table: "Counsling",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OperatedEye",
                table: "Counsling",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackageName",
                table: "Counsling",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
