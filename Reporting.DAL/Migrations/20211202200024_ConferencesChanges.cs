using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reporting.DAL.Migrations
{
    public partial class ConferencesChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DELETE FROM [dbo].[Publications];
DELETE FROM [dbo].[Conferences];");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Conferences",
                newName: "TypeId");

            migrationBuilder.AddColumn<string>(
                name: "CoOrganizers",
                table: "Conferences",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Conferences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Conferences",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfParticipants",
                table: "Conferences",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Organizers",
                table: "Conferences",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Conferences",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubTypeId",
                table: "Conferences",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConferenceSubTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConferenceSubTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConferenceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConferenceTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conferences_DepartmentId",
                table: "Conferences",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Conferences_SubTypeId",
                table: "Conferences",
                column: "SubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Conferences_TypeId",
                table: "Conferences",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conferences_ConferenceSubTypes_SubTypeId",
                table: "Conferences",
                column: "SubTypeId",
                principalTable: "ConferenceSubTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Conferences_ConferenceTypes_TypeId",
                table: "Conferences",
                column: "TypeId",
                principalTable: "ConferenceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conferences_Departments_DepartmentId",
                table: "Conferences",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql(@"
INSERT INTO [dbo].[ConferenceTypes]
            ([Name], [Value])
VALUES      (N'Міжнародні наукові конференції', 1), 
            (N'Всеукраїнські наукові конференції', 2), 
            (N'Наукові семінари', 3), 
            (N'Круглі столи', 4), 
            (N'Внутріуніверситетські наукові заходи', 5), 
            (N'Міжнародні науково-практичні конференції студентів та молодих вчених', 6), 
            (N'Всеукраїнські наукові конференції студентів та молодих вчених', 7),
            (N'Внутріуніверситетські  наукові заходи студентів та молодих вчених', 8);

INSERT INTO [dbo].[ConferenceSubTypes]
            ([Name], [Value])
VALUES      (N'Наукові конференції', 1),
            (N'Наукові семінари', 2),
            (N'Круглі столи', 3);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conferences_ConferenceSubTypes_SubTypeId",
                table: "Conferences");

            migrationBuilder.DropForeignKey(
                name: "FK_Conferences_ConferenceTypes_TypeId",
                table: "Conferences");

            migrationBuilder.DropForeignKey(
                name: "FK_Conferences_Departments_DepartmentId",
                table: "Conferences");

            migrationBuilder.DropTable(
                name: "ConferenceSubTypes");

            migrationBuilder.DropTable(
                name: "ConferenceTypes");

            migrationBuilder.DropIndex(
                name: "IX_Conferences_DepartmentId",
                table: "Conferences");

            migrationBuilder.DropIndex(
                name: "IX_Conferences_SubTypeId",
                table: "Conferences");

            migrationBuilder.DropIndex(
                name: "IX_Conferences_TypeId",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "CoOrganizers",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "NumberOfParticipants",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "Organizers",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "SubTypeId",
                table: "Conferences");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Conferences",
                newName: "Year");
        }
    }
}
