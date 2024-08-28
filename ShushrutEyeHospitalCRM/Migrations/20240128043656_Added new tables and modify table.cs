using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShushrutEyeHospitalCRM.Migrations
{
    public partial class Addednewtablesandmodifytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoctorAdvice",
                table: "PatientHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExaminationDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    FindingName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RightEye = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeftEye = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientAdvGlasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    DistanceRE_Sph = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistanceRE_Cy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistanceRE_Axis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistanceRE_Prism = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistanceRE_VA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistanceRE_NV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistanceLE_Sph = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistanceLE_Cy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistanceLE_Axis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistanceLE_Prism = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistanceLE_VA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistanceLE_NV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddRE_Sph = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddRE_Cy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddRE_Axis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddRE_Prism = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddRE_VA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddRE_NV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddLE_Sph = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddLE_Cy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddLE_Axis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddLE_Prism = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddLE_VA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddLE_NV = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAdvGlasses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminationDetail");

            migrationBuilder.DropTable(
                name: "PatientAdvGlasses");

            migrationBuilder.DropColumn(
                name: "DoctorAdvice",
                table: "PatientHistory");
        }
    }
}
