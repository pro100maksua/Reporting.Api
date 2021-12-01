using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reporting.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    StartPage = table.Column<int>(type: "int", nullable: false),
                    EndPage = table.Column<int>(type: "int", nullable: false),
                    PrintedPagesCount = table.Column<double>(type: "float", nullable: false),
                    ScopusAuthors = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    ConferenceId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publications_Conferences_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "Conferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Publications_PublicationTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PublicationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityIndicator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    ScientificPedagogicalWorkersCount = table.Column<int>(type: "int", nullable: false),
                    FullTimeWorkersCount = table.Column<int>(type: "int", nullable: false),
                    FullTimeDoctorOfScienceWorkersCount = table.Column<int>(type: "int", nullable: false),
                    FullTimeCandidatesOfScienceWorkersCount = table.Column<int>(type: "int", nullable: false),
                    FullTimeNoDegreeWorkersCount = table.Column<int>(type: "int", nullable: false),
                    PartTimeWorkersCount = table.Column<int>(type: "int", nullable: false),
                    PartTimeDoctorOfScienceWorkersCount = table.Column<int>(type: "int", nullable: false),
                    PartTimeCandidatesOfScienceWorkersCount = table.Column<int>(type: "int", nullable: false),
                    PartTimeNoDegreeWorkersCount = table.Column<int>(type: "int", nullable: false),
                    ScientificActivityWorkersCount = table.Column<int>(type: "int", nullable: false),
                    ScientificActivityDoctorOfScienceWorkersCount = table.Column<int>(type: "int", nullable: false),
                    ScientificActivityCandidatesOfScienceWorkersCount = table.Column<int>(type: "int", nullable: false),
                    ScientificActivityNoDegreeWorkersCount = table.Column<int>(type: "int", nullable: false),
                    DoctoralStudentsCount = table.Column<int>(type: "int", nullable: false),
                    GraduateStudentsCount = table.Column<int>(type: "int", nullable: false),
                    GraduateStudentsWithBreakFromProductionCount = table.Column<int>(type: "int", nullable: false),
                    DefendedCandidateDissertationsCount = table.Column<int>(type: "int", nullable: false),
                    DefendedDoctoralDissertationsCount = table.Column<int>(type: "int", nullable: false),
                    StateBudgetFundFinancing = table.Column<double>(type: "float", nullable: false),
                    StateBudgetFundNumberOfWorks = table.Column<int>(type: "int", nullable: false),
                    AtExpenseOfCustomersFinancing = table.Column<double>(type: "float", nullable: false),
                    AtExpenseOfCustomersNumberOfWorks = table.Column<int>(type: "int", nullable: false),
                    InternationalFundsFinancing = table.Column<double>(type: "float", nullable: false),
                    InternationalFundsNumberOfWorks = table.Column<int>(type: "int", nullable: false),
                    CompletedWorksCount = table.Column<int>(type: "int", nullable: false),
                    DevelopmentResultsInProductionCount = table.Column<int>(type: "int", nullable: false),
                    DevelopmentResultsInLearningProcessCount = table.Column<int>(type: "int", nullable: false),
                    ApplicationsForSecurityDocumentsCount = table.Column<int>(type: "int", nullable: false),
                    ReceivedSecurityDocumentsCount = table.Column<int>(type: "int", nullable: false),
                    InventorsCount = table.Column<int>(type: "int", nullable: false),
                    ConferencesSeminarsRoundTablesCount = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityIndicator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityIndicator_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AcademicStatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Position = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IeeeXploreAuthorName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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

            migrationBuilder.CreateTable(
                name: "UserPublications",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "int", nullable: false),
                    PublicationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPublications", x => new { x.AuthorsId, x.PublicationsId });
                    table.ForeignKey(
                        name: "FK_UserPublications_Publications_PublicationsId",
                        column: x => x.PublicationsId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPublications_Users_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Users",
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
                name: "IX_ActivityIndicator_DepartmentId",
                table: "ActivityIndicator",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CreativeConnections_DepartmentId",
                table: "CreativeConnections",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CreativeConnections_TypeId",
                table: "CreativeConnections",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_FacultyId",
                table: "Departments",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Dissertations_AuthorId",
                table: "Dissertations",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Dissertations_DepartmentId",
                table: "Dissertations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_ConferenceId",
                table: "Publications",
                column: "ConferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_TypeId",
                table: "Publications",
                column: "TypeId");

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

            migrationBuilder.CreateIndex(
                name: "IX_UserPublications_PublicationsId",
                table: "UserPublications",
                column: "PublicationsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UsersId",
                table: "UserRoles",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

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
            (N'Наукова публікація у фаховому виданні категорії Б', 5), 
            (N'Наукова публікація у нефаховому науковому журналі або збірнику (категорія В)', 6), 
            (N'Тези доповідей на конференціях', 7), 
            (N'Наукові публікації в міжнародній наукометричній базі даних Scopus', 8), 
            (N'Наукові публікації в міжнародній наукометричній базі даних Web of Science', 9),
            (N'Наукові публікації в зарубіжних виданнях, які не входять у наукометричні бази', 10);

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
            (N'Кафедра цивільного права і процесу', @FacultyId5);

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
            (N'Підготовка дипломних та магістерських робіт дослідницького характеру', 6);

INSERT INTO [dbo].[CreativeConnectionTypes]
            ([Name], [Value])
VALUES      (N'Співпраця', 1), 
            (N'Філіал кафедри', 2);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityIndicator");

            migrationBuilder.DropTable(
                name: "CreativeConnections");

            migrationBuilder.DropTable(
                name: "Dissertations");

            migrationBuilder.DropTable(
                name: "StudentsWorkEntries");

            migrationBuilder.DropTable(
                name: "UserPublications");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "CreativeConnectionTypes");

            migrationBuilder.DropTable(
                name: "StudentsScientificWorkTypes");

            migrationBuilder.DropTable(
                name: "StudentsWorkTypes");

            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Conferences");

            migrationBuilder.DropTable(
                name: "PublicationTypes");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Faculties");
        }
    }
}
