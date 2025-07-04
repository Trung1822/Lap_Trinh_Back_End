namespace BTL1.Models
{
    public class Job
    {
        public int JobID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? CompanyName { get; set; }
        public string? Salary { get; set; }
        public string? Location { get; set; }
        public string? CompanyLogo { get; set; }
        public DateTime PostedDate { get; set; }
    }
}
