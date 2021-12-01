using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reporting.DAL.Migrations
{
    public partial class AddDissertationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dissertations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceOfWork = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Supervisor = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Specialty = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Deadline = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DefenseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DefensePlace = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DiplomaReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuthorId = table.Column<int>(type: "int", nullable: true),
                    AuthorName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dissertations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dissertations_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dissertations_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dissertations_AuthorId",
                table: "Dissertations",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Dissertations_DepartmentId",
                table: "Dissertations",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dissertations");
        }
    }
}
