using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Users.Infrastructure;
using Users.Models;
using Users.Models.Entities;

namespace Users.Infrastructure
{
    public class Repo:IRepo
    {
        public AppIdentityDbContext mycontext = new AppIdentityDbContext();
        public IEnumerable<Content> Contents { get { return mycontext.Contents; } }
        public IEnumerable<CatSubCat> CatSubCats { get { return mycontext.CatSubCats; } }
        public IEnumerable<Subcategory> Subcategories { get { return mycontext.Subcategories; } }
        public IEnumerable<Category> Categories { get { return mycontext.Categories; } }
        public void AddContent(Content Contentpara)
        {

            Content dbEntry = mycontext.Contents.Find(Contentpara.IdContent);
            if (dbEntry == null)
            {
                mycontext.Contents.Add(Contentpara);

            }
            if (dbEntry != null)
            {
                dbEntry.Title = Contentpara.Title;
                dbEntry.Body = Contentpara.Body;
                dbEntry.Byline = Contentpara.Byline;
                dbEntry.CreationDate = Contentpara.CreationDate;
                dbEntry.IdSubcategory = Contentpara.IdSubcategory;
                dbEntry.UserId = Contentpara.UserId;
                dbEntry.ImageData = Contentpara.ImageData;
                dbEntry.ImageType = Contentpara.ImageType;
                dbEntry.Seo = Contentpara.Seo;
                dbEntry.TagLine = Contentpara.TagLine;
                dbEntry.Teaser = Contentpara.Teaser;
                dbEntry.UpdatedDate = Contentpara.UpdatedDate;
                
            }
            mycontext.SaveChanges();

        }
      public  void SaveCategory(Category catepara)
        {
            Category dbEntry = mycontext.Categories.Find(catepara.Idcategory);
            if (dbEntry == null)
            {
                mycontext.Categories.Add(catepara);

            }
            if (dbEntry != null)
            {
                dbEntry.Catename = catepara.Catename;
              

            }
            mycontext.SaveChanges();

        }
        public Settings LoadSetting()
        {
            Settings siteSetting = new Settings(mycontext);
            return siteSetting;

        }

        public void DeleteCategory(int idcategory)
        {
            Category dbEntry = mycontext.Categories.Find(idcategory);

            mycontext.Categories.Remove(dbEntry);
            mycontext.SaveChanges();


        }
        public void DeleteContent(int idcontent)
        {
            Content content = mycontext.Contents.Find(idcontent);

            if(content!=null)
            {
                mycontext.Contents.Remove(content);
                mycontext.SaveChanges();
            }
        }
        public void SaveSubcategory(Subcategory catepara, int idcategory)
        {
            Subcategory dbEntry = mycontext.Subcategories.Find(catepara.IdSubcategory);
            //if the subcate DOESN'T exists, then add a new one.
            if (dbEntry == null)
            {
                mycontext.Subcategories.Add(catepara);

                CatSubCat mycatsubcat = new CatSubCat();
                mycatsubcat.Idcategory = idcategory;
                mycatsubcat.IdSubcategory = catepara.IdSubcategory;
                mycontext.CatSubCats.Add(mycatsubcat);
            }
            //if the subcate exists, then modify
            if (dbEntry != null)
            {
                dbEntry.Subcatename = catepara.Subcatename;
                CatSubCat mycatsubcat = mycontext.CatSubCats.FirstOrDefault(x => x.IdSubcategory == catepara.IdSubcategory);

                    mycatsubcat.Idcategory=idcategory;
                

                

            }
            mycontext.SaveChanges();

        }
   
    }
}