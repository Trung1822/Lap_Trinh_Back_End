namespace BTL1.Models
{
    public class Application
    {
        public int ApplicationID { get; set; }
        public int JobID { get; set; }
        public int CandidateID { get; set; }
        public string? ResumePath { get; set; }
        public DateTime AppliedDate { get; set; }
    }
}
