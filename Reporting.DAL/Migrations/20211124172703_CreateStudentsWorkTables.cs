using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reporting.DAL.Migrations
{
    public partial class CreateStudentsWorkTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentsScientificWorkTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsScientificWorkTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentsWorkTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsWorkTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentsWorkEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Group = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Specialty = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Place = table.Column<int>(type: "int", nullable: true),
                    ScientificWorkName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Independently = table.Column<bool>(type: "bit", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    ScientificWorkTypeId = table.Column<int>(type: "int", nullable: true),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsWorkEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentsWorkEntries_StudentsScientificWorkTypes_ScientificWorkTypeId",
                        column: x => x.ScientificWorkTypeId,
                        principalTable: "StudentsScientificWorkTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentsWorkEntries_StudentsWorkTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "StudentsWorkTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentsWorkEntries_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsWorkEntries_ScientificWorkTypeId",
                table: "StudentsWorkEntries",
                column: "ScientificWorkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsWorkEntries_TeacherId",
                table: "StudentsWorkEntries",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsWorkEntries_TypeId",
                table: "StudentsWorkEntries",
                column: "TypeId");

            migrationBuilder.Sql(@"
INSERT INTO [dbo].[StudentsWorkTypes]
            ([Name], [Value])
VALUES      (N'Участь в науковій роботі', 1), 
            (N'Участь в олімпіадах (1/2 тури)', 2), 
            (N'Одержали нагороди за результатами 2 туру', 3), 
            (N'Участь в наукових конференціях', 4), 
            (N'Участь у конкурсах наукових робіт/одержали нагороди', 5), 
            (N'Участь у конкурсах дипломних і магістерських робіт/одержали нагороди', 6), 
            (N'Опубліковано статтю, тези доповіді', 7);

INSERT INTO [dbo].[StudentsScientificWorkTypes]
            ([Name], [Value])
VALUES      (N'Теоретичний семінар', 1),
            (N'Науковий гурток', 2),
            (N'Проблемна група', 3),
            (N'Інша форма', 4),
            (N'Виконання держбюджетних та госпдоговірних досліджень', 5),
            (N'Підготовка дипломних та магістерських робіт дослідницького характеру', 6);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsWorkEntries");

            migrationBuilder.DropTable(
                name: "StudentsScientificWorkTypes");

            migrationBuilder.DropTable(
                name: "StudentsWorkTypes");
        }
    }
}
