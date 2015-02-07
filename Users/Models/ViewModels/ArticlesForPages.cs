﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Users.Models.Entities;

namespace Users.Models.ViewModels
{
    public class ArticlesForPages
    {
        public IEnumerable<Content> articles { get; set; }
        public Category category { get; set; }
        public Subcategory subcategory { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}