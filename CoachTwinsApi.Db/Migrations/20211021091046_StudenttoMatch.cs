﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoachTwinsApi.Db.Migrations
{
    public partial class StudenttoMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Students_StudentId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_StudentId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Matches");

            migrationBuilder.AddColumn<Guid>(
                name: "MatchId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_MatchId",
                table: "Students",
                column: "MatchId",
                unique: true,
                filter: "[MatchId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Matches_MatchId",
                table: "Students",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Matches_MatchId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_MatchId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "Students");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "Matches",
                type: "uniqueidentifier",
                nullable: true);

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
        }
    }
}
