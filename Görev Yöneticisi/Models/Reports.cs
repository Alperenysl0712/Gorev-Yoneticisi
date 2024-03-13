namespace Görev_Yöneticisi.Models
{
    public class Reports
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string? priorityLevel { get; set; }

        public string? Category { get; set; }

        public string? Header { get; set; }

        public string? Detail { get; set; }

        public DateOnly addedDate { get; set; }

        public DateOnly endDate { get; set; }

        public string? Status { get; set; }



    }
}
