using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Infrastructure
{
    public class GeneralSettings : SettingsBase
    {
        public string SiteName { get; set; }
        public string AdminEmail { get; set; }
    }

    public class SeoSettings : SettingsBase
    {
        public string HomeMetaTitle { get; set; }
        public string HomeMetaDescription { get; set; }
    }
}