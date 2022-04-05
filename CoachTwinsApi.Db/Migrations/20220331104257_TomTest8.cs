using Microsoft.EntityFrameworkCore.Migrations;

namespace CoachTwinsApi.Db.Migrations
{
    public partial class TomTest8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ProfilePicture_PictureId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PictureId",
                table: "Users",
                newName: "ProfilePictureId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_PictureId",
                table: "Users",
                newName: "IX_Users_ProfilePictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ProfilePicture_ProfilePictureId",
                table: "Users",
                column: "ProfilePictureId",
                principalTable: "ProfilePicture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ProfilePicture_ProfilePictureId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ProfilePictureId",
                table: "Users",
                newName: "PictureId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_ProfilePictureId",
                table: "Users",
                newName: "IX_Users_PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ProfilePicture_PictureId",
                table: "Users",
                column: "PictureId",
                principalTable: "ProfilePicture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
