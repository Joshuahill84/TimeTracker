﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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
        public SelectList PossibleShifts { get; set; }
    }

    public class TeamMemberVM
    {
        public string FullName { get; set; }
        [Display(Name = "Check In Time")]
        public TimeClockStatus CheckInTimeStatus { get; set; }
        [Display(Name = "Check Out Time")]
        public TimeClockStatus CheckOutTimeStatus { get; set; }
        public int Id { get; set; }
    }
}