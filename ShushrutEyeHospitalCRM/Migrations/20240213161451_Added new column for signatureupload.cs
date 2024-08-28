using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShushrutEyeHospitalCRM.Migrations
{
    public partial class Addednewcolumnforsignatureupload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Signature",
                table: "Doctors");
        }
    }
}
