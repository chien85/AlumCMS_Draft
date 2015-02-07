using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Users.Models.Entities
{
    public class Category
    {
        public Category()
        {
            this.CatSubCat = new HashSet<CatSubCat>();
        }
        [Key]
        public int Idcategory { get; set; }
        public string Catename { get; set; }

        public virtual ICollection<CatSubCat> CatSubCat { get; set; }
    }
    public class CatSubCat
    {
        public CatSubCat()
        {
            this.Content = new HashSet<Content>();
        }
        [Key]
        public int idcatsubcat { get; set; }
        public Nullable<int> Idcategory { get; set; }
        public Nullable<int> IdSubcategory { get; set; }

        public virtual Category Category { get; set; }
        public virtual Subcategory Subcategory { get; set; }
        public virtual ICollection<Content> Content { get; set; }
    }
    public class Content
    {
        [Key]
        public int IdContent { get; set; }
        public string Title { get; set; }
        public string Byline { get; set; }
        public string Teaser { get; set; }
        public int ViewCount { get; set; }
        [AllowHtml]
        public string Body { get; set; }
        public string TagLine { get; set; }
        public System.DateTime CreationDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public string UserId { get; set; }
        public Nullable<int> IdSubcategory { get; set; }
        public string Seo { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageType { get; set; }
        public virtual AppUser Users { get; set; }

        public virtual Subcategory Subcategory { get; set; }
    }
    public class Subcategory
    {
        public Subcategory()
        {
            this.CatSubCat = new HashSet<CatSubCat>();
        }
        [Key]
        public int IdSubcategory { get; set; }
        public string Subcatename { get; set; }

        public virtual ICollection<CatSubCat> CatSubCat { get; set; }
    }
    public class Setting
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}