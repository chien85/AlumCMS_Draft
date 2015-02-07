using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Users.Infrastructure;
using Users.Models;

namespace Users.Areas.Admin.Controllers
{
    public class DashBoardController : Controller
    {
        //
        // GET: /Admin/DashBoard/
        public ActionResult Index()
        {
            return View();
        }
        
	}
}