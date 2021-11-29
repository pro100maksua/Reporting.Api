using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reporting.DAL.Migrations
{
    public partial class CreateActivityIndicatorsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_ActivityIndicator_DepartmentId",
                table: "ActivityIndicator",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityIndicator");
        }
    }
}
