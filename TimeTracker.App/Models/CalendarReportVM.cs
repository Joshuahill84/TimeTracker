using System;
using System.Collections;
using System.Collections.Generic;

namespace TimeTracker.App.Models
{
    public class CalendarReportVM
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }


        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            if (from.DayOfWeek != DayOfWeek.Sunday)
            {
                int diff = from.DayOfWeek - DayOfWeek.Sunday;
                from = from.AddDays(-1*diff);
            }


            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public IList<CalendarReportDetailVM> Details { get; set; } = new List<CalendarReportDetailVM>();
        public string EmployeeName { get; set; }
        public string TeamName { get; set; }
        public Dictionary<int,string> TeamMembers { get; set; } = new Dictionary<int, string>();
        public int EmployeeId { get; set; }
    }

    public class CalendarReportDetailVM
    {
        public DateTime Day { get; set; }
        public TimeClockStatus CheckInStatus { get; set; }
        public TimeClockStatus CheckOutStatus { get; set; }
        public int EmployeeId { get; set; }
        public int TimeEntryId { get; set; }
        public int ShiftId { get; set; }
        public DayStatus DayStatus { get; set; }
    }
}