using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoachTwinsApi.Db.Migrations
{
    public partial class ProfileReportUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "ProfileReports");

            migrationBuilder.RenameColumn(
                name: "Issuer",
                table: "ProfileReports",
                newName: "ReportedUserId");

            migrationBuilder.RenameColumn(
                name: "CoacheeId",
                table: "ProfileReports",
                newName: "IssuerId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProfileReports",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProfileReports");

            migrationBuilder.RenameColumn(
                name: "ReportedUserId",
                table: "ProfileReports",
                newName: "Issuer");

            migrationBuilder.RenameColumn(
                name: "IssuerId",
                table: "ProfileReports",
                newName: "CoacheeId");

            migrationBuilder.AddColumn<Guid>(
                name: "CoachId",
                table: "ProfileReports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
