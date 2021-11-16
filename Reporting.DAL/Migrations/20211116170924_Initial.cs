using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reporting.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublicationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ScopusAuthorName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_FacultyId",
                table: "Departments",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_TypeId",
                table: "Publications",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UsersId",
                table: "UserRoles",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.Sql(@"
INSERT INTO [dbo].[Roles]
            ([Name], [Value])
VALUES      (N'Викладач', 1),
            (N'Кафедра', 2),
            (N'Факультет', 3);

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

INSERT INTO [dbo].[Faculties]
            ([Name], [Value])
VALUES      (N'Соціально-гуманітарний факультет', 1),
            (N'Факультет економіки та управління', 2),
            (N'Факультет комп''ютерних інформаційних технологій', 3),
            (N'Факультет фінансів та обліку', 4),
            (N'Юридичний факультет', 5);
			
DECLARE @FacultyId1 INT = (SELECT Id FROM [dbo].[Faculties] WHERE [Value] = 1);
DECLARE @FacultyId2 INT = (SELECT Id FROM [dbo].[Faculties] WHERE [Value] = 2);
DECLARE @FacultyId3 INT = (SELECT Id FROM [dbo].[Faculties] WHERE [Value] = 3);
DECLARE @FacultyId4 INT = (SELECT Id FROM [dbo].[Faculties] WHERE [Value] = 4);
DECLARE @FacultyId5 INT = (SELECT Id FROM [dbo].[Faculties] WHERE [Value] = 5);

INSERT INTO [dbo].[Departments]
            ([Name], [FacultyId])
VALUES      (N'Кафедра інформаційної та соціокультурної діяльності', @FacultyId1),
            (N'Кафедра освітології і педагогіки', @FacultyId1),
            (N'Кафедра психології та соціальної роботи', @FacultyId1),

            (N'Кафедра економіки та економічної теорії', @FacultyId2),
            (N'Кафедра менеджменту, публічного управління та персоналу', @FacultyId2),
            (N'Кафедра підприємництва і торгівлі', @FacultyId2),
            (N'Кафедра маркетингу', @FacultyId2),
			
            (N'Кафедра економічної кібернетики та інформатики ', @FacultyId3),
            (N'Кафедра інформаційно-обчислювальних систем і управління', @FacultyId3),
            (N'Кафедра комп''ютерних наук', @FacultyId3),
            (N'Кафедра комп''ютерної інженерії', @FacultyId3),
            (N'Кафедра спеціалізованих комп''ютерних cистем', @FacultyId3),
            (N'Кафедра кібербезпеки', @FacultyId3),
            (N'Кафедра прикладної математики', @FacultyId3),
			
            (N'Кафедра банківського бізнесу', @FacultyId4),
            (N'Кафедра фінансового контролю та аудиту', @FacultyId4),
            (N'Кафедра обліку і оподаткування', @FacultyId4),
            (N'Кафедра податків та фіскальної політики', @FacultyId4),
            (N'Кафедра фінансів ім. С.І. Юрія', @FacultyId4),
            (N'Кафедра фінансового менеджменту та страхування', @FacultyId4),
			
            (N'Кафедра конституційного, адміністративного та фінансового права', @FacultyId5),
            (N'Кафедра кримінального права та процесу', @FacultyId5),
            (N'Кафедра безпеки та правоохоронної діяльності', @FacultyId5),
            (N'Кафедра міжнародного права та міграційної політики', @FacultyId5),
            (N'Кафедра теорії та історії держави і права', @FacultyId5),
            (N'Кафедра цивільного права і процесу', @FacultyId5);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "PublicationTypes");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Faculties");
        }
    }
}
