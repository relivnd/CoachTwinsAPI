using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoachTwinsApi.Db.Migrations
{
    public partial class CriteriaSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characteristics");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "MatchingCriteria");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MatchingCriteria");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "MatchingCriteria");

            migrationBuilder.AddColumn<Guid>(
                name: "MatchId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CriteriaId",
                table: "MatchingCriteria",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CoachId",
                table: "Matches",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Criteria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criteria", x => x.Id);
                    table.UniqueConstraint("AK_Criteria_Category_Value", x => new { x.Category, x.Value });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_MatchId",
                table: "Students",
                column: "MatchId",
                unique: true,
                filter: "[MatchId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MatchingCriteria_CriteriaId",
                table: "MatchingCriteria",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_CoachId",
                table: "Matches",
                column: "CoachId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Coaches_CoachId",
                table: "Matches",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchingCriteria_Criteria_CriteriaId",
                table: "MatchingCriteria",
                column: "CriteriaId",
                principalTable: "Criteria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_MatchingCriteria_Criteria_CriteriaId",
                table: "MatchingCriteria");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Matches_MatchId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Criteria");

            migrationBuilder.DropIndex(
                name: "IX_Students_MatchId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_MatchingCriteria_CriteriaId",
                table: "MatchingCriteria");

            migrationBuilder.DropIndex(
                name: "IX_Matches_CoachId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CriteriaId",
                table: "MatchingCriteria");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "Matches");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "MatchingCriteria",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MatchingCriteria",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "MatchingCriteria",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Characteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characteristics", x => x.Id);
                });
        }
    }
}
