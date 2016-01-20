using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TimeTracker.App.Models;

namespace TimeTracker.App.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            var model = new EmployeeEditVM
            {
                SelectedTeamId = 0,
                AvailableTeams = new SelectList(db.Teams.ToList(), "Id", "Name")
            };

            return View(model);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeEditVM model)
        {
            if (ModelState.IsValid)
            {
                var currentTeam = db.Teams.Find(model.SelectedTeamId);
                var alreadyExists = currentTeam.Employees.Any(x => x.EmployeeNumber == model.EmployeeNumber);
                if (alreadyExists )
                {
                    ModelState.AddModelError("EmployeeNumber", "Person Already Exists with that Employee Number on the Team.");
                    model.AvailableTeams = new SelectList(db.Teams.ToList(), "Id", "Name");
                    return View(model);
                }

                 var   employee = new Employee
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmployeeNumber = model.EmployeeNumber,
                    MemberOf = currentTeam
                };

                db.Employees.Add(employee);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            model.AvailableTeams = new SelectList(db.Teams.ToList(), "Id", "Name");
            return View(model);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            var model = new EmployeeEditVM
            {
                SelectedTeamId = employee.MemberOf?.Id ?? 0,
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                EmployeeNumber = employee.EmployeeNumber,
                AvailableTeams = new SelectList(db.Teams.ToList(), "Id", "Name")
            };

            return View(model);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeEditVM model)
        {
            if (ModelState.IsValid)
            {
                var employee = db.Employees.Find(model.Id);
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.EmployeeNumber = model.EmployeeNumber;
                employee.MemberOf = db.Teams.Find(model.SelectedTeamId);

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            model.AvailableTeams = new SelectList(db.Teams.ToList(), "Id", "Name");
            return View(model);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
