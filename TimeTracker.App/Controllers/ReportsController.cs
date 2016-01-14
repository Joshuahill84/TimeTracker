using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        public ActionResult CalendarReports(int employeeid, DateTime? start, DateTime? end)
        {
            var date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);


            start = start ?? firstDayOfMonth;
            end = end ?? lastDayOfMonth;

            var db = new ApplicationDbContext();
            var timeentries = db.Employees.Find(employeeid).TimeEntries
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
                Start = start.GetValueOrDefault(),
                End = end.GetValueOrDefault(),
                Details = details
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