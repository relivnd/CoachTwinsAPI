using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoachTwinsApi.Db.Migrations
{
    public partial class ProfilePicture_BugFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ProfilePictures_ProfilePictureId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfilePictureId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfilePicture",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ProfilePictures",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProfilePictures");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfilePictureId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfilePictureId",
                table: "Users",
                column: "ProfilePictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ProfilePictures_ProfilePictureId",
                table: "Users",
                column: "ProfilePictureId",
                principalTable: "ProfilePictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
