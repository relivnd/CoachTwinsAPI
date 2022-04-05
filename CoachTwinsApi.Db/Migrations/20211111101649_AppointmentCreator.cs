using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoachTwinsApi.Db.Migrations
{
    public partial class AppointmentCreator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "Appointments",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CreatorId",
                table: "Appointments",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_CreatorId",
                table: "Appointments",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_CreatorId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_CreatorId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Appointments");
        }
    }
}
