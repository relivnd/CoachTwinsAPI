using Microsoft.EntityFrameworkCore.Migrations;

namespace CoachTwinsApi.Db.Migrations
{
    public partial class MatchSplit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isAMatch",
                table: "Matches",
                newName: "isAMatchForCoachee");

            migrationBuilder.AddColumn<bool>(
                name: "isAMatchForCoach",
                table: "Matches",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAMatchForCoach",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "isAMatchForCoachee",
                table: "Matches",
                newName: "isAMatch");
        }
    }
}
