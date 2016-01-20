using System;
using System.ComponentModel.DataAnnotations;

namespace TimeTracker.App.Models
{
    public class TimeEntryEmployeeVM
    {
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
        public string TeamName { get; set; }
        public int TeamId { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        public string FullName { get; set; }
        public TimeClockStatus CheckInTimeStatus { get; set; }
        public TimeClockStatus CheckOutTimeStatus { get; set; }
        public int TimeEntryId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
}