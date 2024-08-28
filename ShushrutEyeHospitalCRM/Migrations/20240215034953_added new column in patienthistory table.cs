using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShushrutEyeHospitalCRM.Migrations
{
    public partial class addednewcolumninpatienthistorytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DistanceVisionLEUnAided",
                table: "PatientHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistanceVisionREUnAided",
                table: "PatientHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NearVisionLEUnAided",
                table: "PatientHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NearVisionREUnAided",
                table: "PatientHistory",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistanceVisionLEUnAided",
                table: "PatientHistory");

            migrationBuilder.DropColumn(
                name: "DistanceVisionREUnAided",
                table: "PatientHistory");

            migrationBuilder.DropColumn(
                name: "NearVisionLEUnAided",
                table: "PatientHistory");

            migrationBuilder.DropColumn(
                name: "NearVisionREUnAided",
                table: "PatientHistory");
        }
    }
}
