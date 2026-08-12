namespace Student_Mangement_System.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalStudents { get; set; }
        public int TotalCourses { get; set; }
        public int TotalTeachers { get; set; }
        public int TotalDepartments { get; set; }
        public int TotalEnrollments { get; set; }
        public List<DepartmentStudentReportViewModel> DepartmentReport { get; set; } = new();
        public List<CourseViewModel> CourseReport { get; set; } = new();
    }
}