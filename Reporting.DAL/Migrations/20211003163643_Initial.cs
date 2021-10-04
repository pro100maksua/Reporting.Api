using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reporting.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PublicationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PublicationTitle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PublicationYear = table.Column<int>(type: "int", nullable: false),
                    PagesCount = table.Column<int>(type: "int", nullable: false),
                    PrintedPagesCount = table.Column<double>(type: "float", nullable: false),
                    Authors = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Doi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Isbn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Abstract = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ArticleNumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PdfUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    HtmlUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ConferenceLocation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CitingPaperCount = table.Column<int>(type: "int", nullable: true),
                    CitingPatentCount = table.Column<int>(type: "int", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publications_PublicationTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PublicationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Publications_TypeId",
                table: "Publications",
                column: "TypeId");

            migrationBuilder.Sql(@"
INSERT INTO [dbo].[PublicationTypes]
            ([Name], [Value])
VALUES      (N'Монографія', 1), 
            (N'Підручник', 2), 
            (N'Навчальний посібник, який рекомендовано Вченою Радою ТНЕУ', 3), 
            (N'Брошура', 4), 
            (N'Наукова публікація у фаховому виданні категорії В', 5), 
            (N'Наукова публікація у нефаховому науковому журналі або збірнику (категорія В)', 6), 
            (N'Тези доповідей на конференціях', 7), 
            (N'Наукові публікації в міжнародній наукометричній базі даних Scopus', 8), 
            (N'Наукові публікації в міжнародній наукометричній базі даних Web of Science', 9);
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropTable(
                name: "PublicationTypes");
        }
    }
}
