namespace Student_Mangement_System.ViewModels
{
    public class ReportViewModel
    {
        public List<DepartmentStudentReportViewModel> DepartmentReport { get; set; } = new();

        public List<CourseViewModel> CourseReport { get; set; } = new();
    }
}
