using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.App.Models
{
    public class TimeEntryVM
    {
        public string  User { get; set; }
        public Team TeamName { get; set; }
        public Employee Employees { get; set; }
        public DateTime Time { get; set; }
        public Shift Shifts { get; set; }
    }
}
