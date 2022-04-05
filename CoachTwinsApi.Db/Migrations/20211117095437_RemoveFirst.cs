using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoachTwinsApi.Db.Migrations
{
    public partial class RemoveFirst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Appointments_FirstAppointmentId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_FirstAppointmentId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "FirstAppointmentId",
                table: "Matches");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FirstAppointmentId",
                table: "Matches",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_FirstAppointmentId",
                table: "Matches",
                column: "FirstAppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Appointments_FirstAppointmentId",
                table: "Matches",
                column: "FirstAppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
