using System;
using System.Collections;
using System.Collections.Generic;

namespace TimeTracker.App.Models
{
    public class TimeEntryIndexVM
    {
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
        public string TeamName { get; set; }
        public int TeamId { get; set; }
        public DateTime Date { get; set; }
        public IList<TeamMemberVM> TeamMembers { get; set; } = new List<TeamMemberVM>();
    }

    public class TeamMemberVM
    {
        public string FullName { get; set; }
        public TimeClockStatus CheckInTimeStatus { get; set; }
        public TimeClockStatus CheckOutTimeStatus { get; set; }
    }
}