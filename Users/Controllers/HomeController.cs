using System.Web.Mvc;
using System.Collections.Generic;
using Users.Infrastructure;
using Users.Models.ViewModels;
using Users.Models.Entities;
using System.Data.Entity;
using System.Linq;
namespace Users.Controllers {

    public class HomeController : Controller {

        IRepo myrepo;
        public int PageSize = 4;

        public HomeController(IRepo repopara)
        {
            myrepo = repopara;
        }
        public ActionResult Index() {
            Dictionary<string, object> data
                = new Dictionary<string, object>();
            data.Add("Placeholder", "Placeholder");
            return View(data);
        }
        public ActionResult Main()
        {
            Settings mysetting = myrepo.LoadSetting();

            ViewBag.username = HttpContext.User.Identity.Name;
            ViewBag.HomeSeoMeta = mysetting.Seo.HomeMetaDescription;
            ViewBag.AdminEmail = mysetting.General.AdminEmail;

            
            ArticlesForHomeControllerModel articleforhome = new ArticlesForHomeControllerModel
            {
                articles = myrepo.Contents.OrderBy(x=>x.IdContent).Take(6),
                Intro=mysetting.General.SiteName,
                

            
            };
            return View(articleforhome);
        }
        private void LoadSetting()
        {
            
        }
        public ActionResult Content(int idcontent, string slug)
        {
           
            var content=myrepo.Contents.FirstOrDefault(x=>x.IdContent==idcontent);
            //content.ViewCount = content.ViewCount + 1;
               if(string.IsNullOrEmpty(slug)){
                   slug = content.Seo;
                   return RedirectToRoute("Article", new { idcontent = content.IdContent, slug = slug });
    }
            return View(content);
        }
      
        public ActionResult Pages(int idcategory, int? idsub, int page=1)
        {
            
            if(idsub!=null && idsub!=0)// it means that has format idcate/idsub
            {
                ArticlesForPages myarticles = new ArticlesForPages();

                myarticles.articles = myrepo.Contents.Where(x => x.IdSubcategory == idsub).Skip((page-1)*PageSize).Take(PageSize);
                myarticles.category = myrepo.Categories.FirstOrDefault(x => x.Idcategory == idcategory);
                myarticles.subcategory = myrepo.Subcategories.FirstOrDefault(x => x.IdSubcategory == idsub);
                PagingInfo paging = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = myrepo.Contents.Where(x=>x.IdSubcategory==idsub).Count()

                };
                myarticles.PagingInfo = paging;

                return View(myarticles);
            }

            else //it means that has format only /idcate
            {
                ArticlesForPages myarticles = new ArticlesForPages();
                
                int?[] idsubcategory = myrepo.CatSubCats.Where(x => x.Idcategory == idcategory).Select(x => x.IdSubcategory).ToArray();

                myarticles.articles=
                 myrepo.Contents.Where(x => idsubcategory.Contains(x.IdSubcategory)).Skip((page - 1) * PageSize).Take(PageSize);
                myarticles.category = myrepo.Categories.FirstOrDefault(x => x.Idcategory == idcategory);
                                myarticles.subcategory = null;

                                PagingInfo mypaging = new PagingInfo
                                {
                                    CurrentPage = page,
                                    ItemsPerPage = PageSize,
                                    TotalItems = myrepo.Contents.Where(x=>idsubcategory.Contains(x.IdSubcategory)).Count()

                                };
                                myarticles.PagingInfo = mypaging;
                return View(myarticles);
            }
        }
        public ActionResult Cates(int idcategory, int page = 1)
        {
                        
                ArticlesForPages myarticles = new ArticlesForPages();

                int?[] idsubcategory = myrepo.CatSubCats.Where(x => x.Idcategory == idcategory).Select(x => x.IdSubcategory).ToArray();

                myarticles.articles =
                 myrepo.Contents.Where(x => idsubcategory.Contains(x.IdSubcategory)).Skip((page - 1) * PageSize).Take(PageSize);
                myarticles.category = myrepo.Categories.FirstOrDefault(x => x.Idcategory == idcategory);
                myarticles.subcategory = null;

                PagingInfo mypaging = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = myrepo.Contents.Where(x => idsubcategory.Contains(x.IdSubcategory)).Count()

                };
                myarticles.PagingInfo = mypaging;
                return View(myarticles);
            
        }
    }
}
