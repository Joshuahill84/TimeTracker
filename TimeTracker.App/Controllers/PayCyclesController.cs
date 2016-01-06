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
    public class PayCyclesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PayCycles
        public ActionResult Index()
        {
            return View(db.PayCycles.ToList());
        }

        // GET: PayCycles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayCycle payCycle = db.PayCycles.Find(id);
            if (payCycle == null)
            {
                return HttpNotFound();
            }
            return View(payCycle);
        }

        // GET: PayCycles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PayCycles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] PayCycle payCycle)
        {
            if (ModelState.IsValid)
            {
                db.PayCycles.Add(payCycle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(payCycle);
        }

        // GET: PayCycles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayCycle payCycle = db.PayCycles.Find(id);
            if (payCycle == null)
            {
                return HttpNotFound();
            }
            return View(payCycle);
        }

        // POST: PayCycles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] PayCycle payCycle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payCycle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payCycle);
        }

        // GET: PayCycles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayCycle payCycle = db.PayCycles.Find(id);
            if (payCycle == null)
            {
                return HttpNotFound();
            }
            return View(payCycle);
        }

        // POST: PayCycles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PayCycle payCycle = db.PayCycles.Find(id);
            db.PayCycles.Remove(payCycle);
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
