using Microsoft.EntityFrameworkCore.Migrations;

namespace CoachTwinsApi.Db.Migrations
{
    public partial class CascadeMC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchingCriteria_Users_UserId",
                table: "MatchingCriteria");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchingCriteria_Users_UserId",
                table: "MatchingCriteria",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchingCriteria_Users_UserId",
                table: "MatchingCriteria");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchingCriteria_Users_UserId",
                table: "MatchingCriteria",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
