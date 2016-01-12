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
                    CheckInTimeStatus = TimeClockStatus.UnKnown,
                    CheckOutTimeStatus = TimeClockStatus.Early,
                    FullName = $"{e.FirstName} {e.LastName}",
                    Id = e.Id,
                });

            }

            return View(model);
        }

        public ActionResult EmployeeEntry(int employeeid, int shiftid, DateTime date)
        {


            return View();
        }
    }
}