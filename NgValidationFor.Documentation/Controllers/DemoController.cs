using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NgValidationFor.Documentation.Models;

namespace NgValidationFor.Documentation.Controllers
{
    public class DemoController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Required()
        {
            var requiredModel = new RequiredDemoModel();
            return View(requiredModel);
        }

    }
}