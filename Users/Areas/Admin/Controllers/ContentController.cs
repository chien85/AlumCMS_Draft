using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Users.Infrastructure;
using Users.Models;
using Users.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Users.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Admin/Content/
        string username;

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppRoleManager>();
            }
        }
        IRepo myrepo;
        public ContentController(IRepo repopara)
        {
            myrepo = repopara;
           
        }
        public ActionResult Index()
        {
            return View(myrepo.Contents);
        }
        public ActionResult Edit(int IdContent)
        {
            Content mycontent = myrepo.Contents.FirstOrDefault(x => x.IdContent == IdContent);

            ViewBag.catelist = new SelectList(myrepo.Categories.Select(x => new { x.Idcategory, x.Catename }), "Idcategory", "Catename", 1);

            ViewBag.subcatelist = new SelectList(myrepo.Subcategories.Select(x => new { x.IdSubcategory, x.Subcatename }), "IdSubcategory", "Subcatename", 1);

                return View(mycontent);


        }
        [HttpPost]
        public ActionResult Edit(Content Contentpara, HttpPostedFileBase image = null)
        {
           

            if(ModelState.IsValid)
            {
                if (image != null)
                {
                    Contentpara.ImageType = image.ContentType;
                    Contentpara.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(Contentpara.ImageData, 0, image.ContentLength);
                    myrepo.AddContent(Contentpara);

                }
                else
                {
                    myrepo.AddContent(Contentpara);
                }


                return RedirectToAction("Index");
            }
            else
            {
                return View(Contentpara);
            }
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(Content contentpara, HttpPostedFileBase image = null)
        {
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                username = HttpContext.User.Identity.Name;
            }
            
            if(ModelState.IsValid)
            {
                contentpara.UserId = UserManager.FindByName(username).Id;
                contentpara.Seo = contentpara.Title.GenerateSlug();
            
            if (image != null)
            {
                contentpara.ImageType = image.ContentType;
                contentpara.ImageData = new byte[image.ContentLength];
                image.InputStream.Read(contentpara.ImageData, 0, image.ContentLength);
                myrepo.AddContent(contentpara);


            }
            else
            {
                myrepo.AddContent(contentpara);
            }
            }

            return RedirectToAction("Index");

        }
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.catelist = new SelectList(myrepo.Categories.Select(x => new { x.Idcategory, x.Catename }), "Idcategory", "Catename", 1);

            ViewBag.subcatelist = new SelectList(GetSubCategories(1).Select(x => new { x.IdSubcategory, x.Subcatename }), "IdSubcategory", "Subcatename", 1);
            //ViewBag.catelist = new SelectList(myrepo.Categories.Select(x => new { x.Idcategory, x.Catename }), "Idcategory", "Catename", 1);

            //ViewBag.subcatelist = new SelectList(myrepo.Subcategories.Select(x => new { x.IdSubcategory, x.Subcatename }), "IdSubcategory", "Subcatename", 1);
            return View(new Content());

        }
        public ActionResult Test()
        {
            ViewBag.catelist = new SelectList(myrepo.Categories.Select(x => new { x.Idcategory, x.Catename }), "Idcategory", "Catename", 1);

            ViewBag.subcatelist = new SelectList(GetSubCategories(1).Select(x=> new {x.IdSubcategory,x.Subcatename}), "IdSubcategory", "Subcatename", 1);
            
            //Content mycontent = myrepo.Contents.FirstOrDefault(x => x.IdContent == 1);
            //int? idcategory = 0;
            //int? idsubcategory = 0;

            //string namecategory, namesubcategory;
            //int? SelectedIdCatSubCat = mycontent.idcatsubcat;
            //if (SelectedIdCatSubCat != null)
            //{
            //    int idcatsubcat = (int)SelectedIdCatSubCat;
            //    idcategory = myrepo.CatSubCats.Where(x => x.idcatsubcat == idcatsubcat).Select(x => x.Idcategory).FirstOrDefault();
            //    idsubcategory = myrepo.CatSubCats.Where(x => x.idcatsubcat == idcatsubcat).Select(x => x.IdSubcategory).FirstOrDefault();


            //}

            //namecategory = myrepo.Categories.Where(x => x.Idcategory == idcategory).Select(x => x.Catename).FirstOrDefault();
            //namesubcategory = myrepo.Subcategories.Where(x => x.IdSubcategory == idsubcategory).Select(x => x.Subcatename).FirstOrDefault();

            //SelectList categorylist = new SelectList(myrepo.Categories.ToList());


            //ViewBag.catelist = new SelectList( myrepo.Categories.Select(x => new { x.Idcategory,x.Catename}),"Idcategory","Catename",2);
            //ViewBag.listest = new SelectList(new[] { "Duoc lam", "kha lam", "Good job" });

            return View();

        }
[HttpGet]
        public JsonResult LoadSubCate(int idcate)
        {
            var listSubcate = GetSubCategories(idcate);
            var listsubcate = listSubcate.Select(m => new SelectListItem { Text = m.Subcatename, Value = m.IdSubcategory.ToString() });

            return Json(listsubcate, JsonRequestBehavior.AllowGet);

        }

        
        public IEnumerable<Subcategory> GetSubCategories(int idcategory)
        {
            int?[] idsub = myrepo.CatSubCats.Where(x => x.Idcategory == idcategory).Select(x => x.IdSubcategory).ToArray();


            IEnumerable<Subcategory> mysubcate = myrepo.Subcategories.Where(x => idsub.Contains(x.IdSubcategory));

            return mysubcate;
        }
        public Category getCategory(int idsubcate)
        {
            int? idcate= myrepo.CatSubCats.Where(x => x.IdSubcategory == idsubcate).Select(x => x.Idcategory).First();

            return myrepo.Categories.Where(x => x.Idcategory == idcate).First();


        }
        public FileContentResult GetImage(int IdContent)
        {
            Content mycontent = myrepo.Contents
                .FirstOrDefault(p => p.IdContent == IdContent);
            if (mycontent != null)
            {
                return File(mycontent.ImageData, mycontent.ImageType);
            }
            else
            {
                return null;
            }
        }
        public ActionResult Upload()
        {
            var file = Request.Files["Filedata"];
            string savePath = Server.MapPath(@"~\Content\" + file.FileName);
            file.SaveAs(savePath);

            return Content(Url.Content(@"~\Content\" + file.FileName));
        }

        public ActionResult Delete(int idcontent)
        {
            myrepo.DeleteContent(idcontent);
            return RedirectToAction("Index");

        }
	}
}