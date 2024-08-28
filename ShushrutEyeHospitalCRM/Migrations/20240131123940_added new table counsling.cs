using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShushrutEyeHospitalCRM.Migrations
{
    public partial class addednewtablecounsling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CounslingId",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Counsling",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counsling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Counsling_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_CounslingId",
                table: "Patients",
                column: "CounslingId");

            migrationBuilder.CreateIndex(
                name: "IX_Counsling_UserId",
                table: "Counsling",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Counsling_CounslingId",
                table: "Patients",
                column: "CounslingId",
                principalTable: "Counsling",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Counsling_CounslingId",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "Counsling");

            migrationBuilder.DropIndex(
                name: "IX_Patients_CounslingId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CounslingId",
                table: "Patients");
        }
    }
}
