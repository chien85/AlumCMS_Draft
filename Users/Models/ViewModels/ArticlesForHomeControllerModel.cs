using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Users.Models.Entities;
namespace Users.Models.ViewModels
{
    public class ArticlesForHomeControllerModel
    {
        public IEnumerable<Content> articles { get; set; }
        public string Intro { get; set; }
        public PagingInfo PagingInfo { get; set; }

    }
}