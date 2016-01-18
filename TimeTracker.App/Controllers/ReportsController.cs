using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TimeTracker.App.Migrations;
using TimeTracker.App.Models;

namespace TimeTracker.App.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CalendarReports(int? employeeid, DateTime? monthyear)
        {
            var date = monthyear ?? DateTime.Now;
            var start = new DateTime(date.Year, date.Month, 1);
            var end = start.AddMonths(1).AddDays(-1);


            var db = new ApplicationDbContext();
            var currentUser = db.Users.Find(User.Identity.GetUserId()); //ApplicationUser
            if (employeeid == null)
            {
                employeeid = currentUser.OwnerOf.Employees.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).FirstOrDefault()?.Id ?? 1; //NOT 1 but the id of the FIRST() team member currentUser.OwnerOf.Name.First();
            }

            var employee = db.Employees.Find(employeeid);
            var timeentries = employee.TimeEntries
                    .Where(x => x.Day > start && x.Day <= end);

            var details =
                timeentries.Select(
                    te =>
                        new CalendarReportDetailVM()
                        {
                            Day = te.Day,
                            CheckInStatus = te.CheckInStatus,
                            CheckOutStatus = te.CheckOutStatus,
                            EmployeeId = te.Employee.Id,
                            TimeEntryId = te.Id,
                            ShiftId = te.Shift.Id,
                            DayStatus = te.DayStatus
                        }).ToList();


            var model = new CalendarReportVM
            {
                Start = start,
                End = end,
                Details = details,
                EmployeeId = employee.Id,
                EmployeeName = $"{employee.FirstName} {employee.LastName}",
                TeamName = employee.MemberOf.Name,
                TeamMembers = currentUser.OwnerOf.Employees
                        .Select(x => new { Id = x.Id, FullName = x.FirstName + " " + x.LastName }).ToDictionary(x => x.Id, x => x.FullName)
            };


            return View(model);
        }


        public ActionResult Test()
        {

            var db = new ApplicationDbContext();
            var model = db.TimeEntries.Select(te => new
            {
                x = SqlFunctions.DatePart("yyyy", te.Day) + "-" + SqlFunctions.DatePart("mm", te.Day) + "-" + SqlFunctions.DatePart("dd", te.Day),
                y = te.Employee.FirstName,
                value = (int)te.CheckInStatus
            }).ToList();

            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }
    }
}