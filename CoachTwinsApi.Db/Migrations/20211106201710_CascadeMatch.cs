using Microsoft.EntityFrameworkCore.Migrations;

namespace CoachTwinsApi.Db.Migrations
{
    public partial class CascadeMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Coaches_CoachId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Matches_MatchId",
                table: "Students");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Coaches_CoachId",
                table: "Matches",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Matches_MatchId",
                table: "Students",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Coaches_CoachId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Matches_MatchId",
                table: "Students");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Coaches_CoachId",
                table: "Matches",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Matches_MatchId",
                table: "Students",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
