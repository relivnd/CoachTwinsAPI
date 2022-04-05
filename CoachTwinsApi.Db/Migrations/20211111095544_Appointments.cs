using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoachTwinsApi.Db.Migrations
{
    public partial class Appointments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MatchId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MatchId",
                table: "Appointments",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Matches_MatchId",
                table: "Appointments",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Matches_MatchId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_MatchId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "Appointments");
        }
    }
}
