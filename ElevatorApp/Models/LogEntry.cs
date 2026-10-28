using System;

namespace ElevatorApp.Models
{
    public class LogEntry
    {
        public int Id { get; set; }
        public string ElevatorName { get; set; }
        public int Floor { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
