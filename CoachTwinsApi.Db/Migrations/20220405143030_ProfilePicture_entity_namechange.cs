using Microsoft.EntityFrameworkCore.Migrations;

namespace CoachTwinsApi.Db.Migrations
{
    public partial class ProfilePicture_entity_namechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ProfilePicture_ProfilePictureId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfilePicture",
                table: "ProfilePicture");

            migrationBuilder.RenameTable(
                name: "ProfilePicture",
                newName: "ProfilePictures");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfilePictures",
                table: "ProfilePictures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ProfilePictures_ProfilePictureId",
                table: "Users",
                column: "ProfilePictureId",
                principalTable: "ProfilePictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ProfilePictures_ProfilePictureId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfilePictures",
                table: "ProfilePictures");

            migrationBuilder.RenameTable(
                name: "ProfilePictures",
                newName: "ProfilePicture");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfilePicture",
                table: "ProfilePicture",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ProfilePicture_ProfilePictureId",
                table: "Users",
                column: "ProfilePictureId",
                principalTable: "ProfilePicture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
