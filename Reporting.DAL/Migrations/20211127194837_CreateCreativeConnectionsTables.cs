using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reporting.DAL.Migrations
{
    public partial class CreateCreativeConnectionsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreativeConnectionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreativeConnectionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreativeConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreativeConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreativeConnections_CreativeConnectionTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "CreativeConnectionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreativeConnections_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreativeConnections_DepartmentId",
                table: "CreativeConnections",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CreativeConnections_TypeId",
                table: "CreativeConnections",
                column: "TypeId");

            migrationBuilder.Sql(@"
INSERT INTO [dbo].[CreativeConnectionTypes]
            ([Name], [Value])
VALUES      (N'Співпраця', 1), 
            (N'Філіал кафедри', 2);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreativeConnections");

            migrationBuilder.DropTable(
                name: "CreativeConnectionTypes");
        }
    }
}
