using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTracker.App.Models;

namespace TimeTracker.App.Controllers
{
    [Authorize]
    public class TimeEntryController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: TimeEntry
        public ActionResult Index(int teamId, int shiftId)
        {
            var shift = db.Shifts.Find(shiftId);
            var team = db.Teams.Find(teamId);
            var model = new TimeEntryIndexVM
            {
                Date = DateTime.Now,
                ShiftName = shift.Name,
                ShiftId = shift.Id,
                TeamName = team.Name,
                TeamId = team.Id,
            };

            foreach (var e in team.Employees)
            {
                model.TeamMembers.Add(new TeamMemberVM()
                {
                    FullName = $"{e.FirstName} {e.LastName}",
                    Id = e.Id,
                });

            }

            return View(model);
        }
        //GET
        [HttpGet]
        public ActionResult EmployeeEntry(int employeeid, int shiftid, DateTime date)
        {

            var shift = db.Shifts.Find(shiftid);
            //var team = db.Teams.Find(teamId);
            var employee = db.Employees.Find(employeeid);
            var model = new TimeEntryEmployeeVM()
            {
                Date = DateTime.Now,
                ShiftName = shift.Name,
                ShiftId = shift.Id,
                TeamName = employee.MemberOf.Name,
                TeamId = employee.MemberOf.Id,
                EmployeeName = $"{employee.FirstName} {employee.LastName}",
                EmployeeId = employee.Id,
            };
            return View(model);
        }


        [HttpPost]
        public ActionResult EmployeeEntry(TimeEntryEmployeeVM entry)
        {

            var shift = db.Shifts.Find(entry.ShiftId);
            var employee = db.Employees.Find(entry.EmployeeId);

            //Look for an existing time entry for this day and shift for this employee.
            var timeEntry = employee.TimeEntries.FirstOrDefault(te => te.Day.Date == entry.Date.Date && te.Shift == shift);

            if (timeEntry == null) //there isn't an existing time entry, so create a new one.
            {
                timeEntry = new TimeEntry
                {
                    Employee = employee,
                    Shift = shift,
                    Day = entry.Date.Date
                };

                employee.TimeEntries.Add(timeEntry);
            }


            timeEntry.BillableHours = 0;
            timeEntry.DayStatus = DayStatus.Present;
            timeEntry.CheckInStatus = entry.CheckInTimeStatus;
            timeEntry.CheckOutStatus = entry.CheckOutTimeStatus;


            db.SaveChanges();

            return RedirectToAction("Index", new {shiftid = entry.ShiftId, teamid = entry.TeamId});


        }

    }
}