namespace Student_Mangement_System.ViewModels
{
    public class DepartmentStudentReportViewModel
    {
        //Department Report
        public string DepartmentName { get; set; } = null!;
        public int StudentCount { get; set; }
        public List<string> StudentNames { get; set; } = new();
        
    }
}
