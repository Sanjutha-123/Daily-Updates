namespace DailyUpdates.Models
{
    public class Report
    {
        public int Id { get; set; }

        public string? Signin{ get; set; }
        public string Task { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Issues { get; set; } = string.Empty;  

        public string?Signout { get; set; } 

        public DateTime ReportDate { get; set; }
        public int UserId { get; internal set; }
    }
}