using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoachTwinsApi.Db.Migrations
{
    public partial class OptionalMatchAndBirthDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchingCriteria_Criteria_CriteriaId",
                table: "MatchingCriteria");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Matches_MatchId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_MatchId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_MatchingCriteria_CriteriaId",
                table: "MatchingCriteria");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Criteria_Category_Value",
                table: "Criteria");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Criteria",
                table: "Criteria");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CriteriaId",
                table: "MatchingCriteria");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Criteria");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Criteria");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CriteriaCategory",
                table: "MatchingCriteria",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "MatchingCriteria",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "Matches",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CriteriaEvaluationType",
                table: "Criteria",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Criteria",
                table: "Criteria",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_MatchingCriteria_CriteriaCategory",
                table: "MatchingCriteria",
                column: "CriteriaCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_StudentId",
                table: "Matches",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Students_StudentId",
                table: "Matches",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchingCriteria_Criteria_CriteriaCategory",
                table: "MatchingCriteria",
                column: "CriteriaCategory",
                principalTable: "Criteria",
                principalColumn: "Category",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Students_StudentId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchingCriteria_Criteria_CriteriaCategory",
                table: "MatchingCriteria");

            migrationBuilder.DropIndex(
                name: "IX_MatchingCriteria_CriteriaCategory",
                table: "MatchingCriteria");

            migrationBuilder.DropIndex(
                name: "IX_Matches_StudentId",
                table: "Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Criteria",
                table: "Criteria");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CriteriaCategory",
                table: "MatchingCriteria");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "MatchingCriteria");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "CriteriaEvaluationType",
                table: "Criteria");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "Id",
                table: "Criteria",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Criteria",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Criteria_Category_Value",
                table: "Criteria",
                columns: new[] { "Category", "Value" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Criteria",
                table: "Criteria",
                column: "Id");

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
    }
}
