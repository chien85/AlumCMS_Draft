using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Users.Infrastructure
{
   
        public static class Extentions
        {
            public static string GenerateSlug(this string phrase)
            {
                string str = phrase.Slug().ToLower();
                // invalid chars           
                str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
                // convert multiple spaces into one space   
                str = Regex.Replace(str, @"\s+", " ").Trim();
                // cut and trim 
                str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
                str = Regex.Replace(str, @"\s", "-"); // hyphens   
                return str;
            }

            public static string Slug(this string ip_str_change)
            {
                Regex v_reg_regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
                string v_str_FormD = ip_str_change.Normalize(NormalizationForm.FormD);
                return v_reg_regex.Replace(v_str_FormD, String.Empty)
       .Replace('\u0111', 'd').Replace('\u0110', 'D');
            }

        }

    
}