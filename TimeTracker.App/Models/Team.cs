﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.App.Models
{
    public class Employee : Entity
    {
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual Team MemberOf { get; set; }
        public virtual ICollection<TimeEntry> TimeEntries { get; set; }
    }

    public class Team : Entity
    {
        public string Name { get; set; }
        public ApplicationUser Owner { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }

    public class TimeEntry : Entity
    {
        public virtual Employee Employee { get; set; }
        public virtual Shift Shift { get; set; }

        public DateTime Day { get; set; }

        public TimeClockStatus CheckInStatus { get; set; }
        public TimeClockStatus CheckOutStatus { get; set; }

        public DayStatus DayStatus { get; set; }

        public int BillableHours { get; set; }
    }

    public enum TimeClockStatus
    {

        Unknown = 0,

        Early,
        [Display(Name = "On Time")]
        OnTime,
        Late,
    }

    public enum DayStatus
    {
        Unknown = 0, 
        NoShow,
        [Display(Name = "Sick Leave")]
        SickLeave,
        PTO,
        Present 
    }

    public class Position : Entity
    {
        public string Name { get; set; }
    }

    public class Shift : Entity
    {
        public string Name { get; set; }
    }

    public class PayCycle : Entity
    {
        public string Name { get; set; }
    }
}
