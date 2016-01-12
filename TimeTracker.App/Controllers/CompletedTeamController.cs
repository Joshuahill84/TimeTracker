using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeTracker.App.Controllers
{
    public class CompletedTeamController : Controller
    {
        // GET: CompletedTeam
        public ActionResult Index()
        {
            return View();
        }
    }
}