using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Users.Models.ViewModels
{
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password{get;set;}
        [Required]
        public string ConfirmPassword { get; set; }

        public DateTime BirthDate { get; set; }
        public string Phoneno { get; set; }
    }
}