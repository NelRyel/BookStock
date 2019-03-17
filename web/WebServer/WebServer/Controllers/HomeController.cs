using StockEntModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebServer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            using (StockDBcontext stockDBcontext = new StockDBcontext())
            {
                var s = stockDBcontext.Custumers.Where(i => i.IsDelete == false);
                ViewBag.SomeShit = s.ToList();
            }
            return View();
        }
    }
}
