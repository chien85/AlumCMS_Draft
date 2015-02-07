using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Users.Models.Entities;
using Users.Infrastructure;
using Users.Models;

namespace Users.Areas.Admin.Controllers
{
    public class SettingController : Controller
    {
        //
        // GET: /Admin/Setting/
        IRepo myrepo;
        Settings mysetting;
        public SettingController(IRepo repopara)
        {
            myrepo = repopara;
        }

        [HttpGet]
        public ActionResult Index()
        {
            mysetting = myrepo.LoadSetting();
            ViewBag.SiteName=mysetting.General.SiteName;
            ViewBag.AdminEmail=mysetting.General.AdminEmail;
            ViewBag.MetaDesc=mysetting.Seo.HomeMetaDescription;
            ViewBag.MetaTitle=mysetting.Seo.HomeMetaTitle;

            return View();
                        
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            Settings xsetting = myrepo.LoadSetting();
                xsetting.General.SiteName = form["SiteName"].ToString();

                xsetting.General.AdminEmail = form["AdminEmail"].ToString();
                xsetting.Seo.HomeMetaDescription = form["MetaDesc"].ToString();

               xsetting.Seo.HomeMetaTitle = form["MetaTitle"].ToString();
                xsetting.Save();

            

            return View();
        }
        

	}
}