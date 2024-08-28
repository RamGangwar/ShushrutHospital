using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShushrutEyeHospitalCRM.Migrations
{
    public partial class addednewcolumninpataienttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OtherProblem",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherProblem",
                table: "Patients");
        }
    }
}
