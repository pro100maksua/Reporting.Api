namespace Reporting.Domain.Entities
{
    public class ActivityIndicator : AuditableEntity
    {
        public int Id { get; set; }
        public int Year { get; set; }

        public int ScientificPedagogicalWorkersCount { get; set; }

        public int FullTimeWorkersCount { get; set; }
        public int FullTimeDoctorOfScienceWorkersCount { get; set; }
        public int FullTimeCandidatesOfScienceWorkersCount { get; set; }
        public int FullTimeNoDegreeWorkersCount { get; set; }

        public int PartTimeWorkersCount { get; set; }
        public int PartTimeDoctorOfScienceWorkersCount { get; set; }
        public int PartTimeCandidatesOfScienceWorkersCount { get; set; }
        public int PartTimeNoDegreeWorkersCount { get; set; }

        public int ScientificActivityWorkersCount { get; set; }
        public int ScientificActivityDoctorOfScienceWorkersCount { get; set; }
        public int ScientificActivityCandidatesOfScienceWorkersCount { get; set; }
        public int ScientificActivityNoDegreeWorkersCount { get; set; }

        public int DoctoralStudentsCount { get; set; }
        public int GraduateStudentsCount { get; set; }
        public int GraduateStudentsWithBreakFromProductionCount { get; set; }

        public int DefendedCandidateDissertationsCount { get; set; }
        public int DefendedDoctoralDissertationsCount { get; set; }

        public double StateBudgetFundFinancing { get; set; }
        public int StateBudgetFundNumberOfWorks { get; set; }

        public double AtExpenseOfCustomersFinancing { get; set; }
        public int AtExpenseOfCustomersNumberOfWorks { get; set; }

        public double InternationalFundsFinancing { get; set; }
        public int InternationalFundsNumberOfWorks { get; set; }

        public int CompletedWorksCount { get; set; }

        public int DevelopmentResultsInProductionCount { get; set; }
        public int DevelopmentResultsInLearningProcessCount { get; set; }

        public int ApplicationsForSecurityDocumentsCount { get; set; }
        public int ReceivedSecurityDocumentsCount { get; set; }

        public int InventorsCount { get; set; }

        public int ConferencesSeminarsRoundTablesCount { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
