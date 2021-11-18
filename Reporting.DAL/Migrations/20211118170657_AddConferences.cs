using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reporting.DAL.Migrations
{
    public partial class AddConferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConferenceId",
                table: "Publications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Conferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Number = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conferences", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Publications_ConferenceId",
                table: "Publications",
                column: "ConferenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Conferences_ConferenceId",
                table: "Publications",
                column: "ConferenceId",
                principalTable: "Conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Conferences_ConferenceId",
                table: "Publications");

            migrationBuilder.DropTable(
                name: "Conferences");

            migrationBuilder.DropIndex(
                name: "IX_Publications_ConferenceId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "ConferenceId",
                table: "Publications");
        }
    }
}
