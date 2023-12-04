namespace Lafarge_TimeSheet.Models
{
    public class TimeEntry
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } = string.Empty;
        public DateTime? ClockInTime { get; set; }
        public DateTime? ClockOutTime { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
    }
}
