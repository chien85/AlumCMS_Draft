using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Users.Models.Entities;
using Users.Models;
namespace Users.Infrastructure
{
    public interface IRepo
    {
        IEnumerable<Content> Contents { get; }
        IEnumerable<CatSubCat> CatSubCats { get; }
        IEnumerable<Subcategory> Subcategories { get; }
        IEnumerable<Category> Categories { get; }

        void AddContent(Content contentpara);
        void DeleteContent(int idcontent);
        Settings LoadSetting();
        void SaveCategory(Category catepara);
        void DeleteCategory(int idcategory);
        void SaveSubcategory(Subcategory catepara, int idcategory);
    }
}