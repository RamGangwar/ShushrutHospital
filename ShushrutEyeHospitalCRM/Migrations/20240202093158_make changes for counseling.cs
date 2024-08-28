using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShushrutEyeHospitalCRM.Migrations
{
    public partial class makechangesforcounseling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CounselingStatus",
                table: "Patients",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PatientCounseling",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    MRNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatedEye = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcedureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalProcedure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnesthesiaType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientOrParty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproxCharge = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DoctorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CounsellorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SurgeryDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientCounseling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientCounseling_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientCounseling_PatientId",
                table: "PatientCounseling",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientCounseling");

            migrationBuilder.DropColumn(
                name: "CounselingStatus",
                table: "Patients");
        }
    }
}
