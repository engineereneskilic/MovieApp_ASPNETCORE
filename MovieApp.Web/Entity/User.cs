﻿using System.ComponentModel.DataAnnotations;

namespace MovieApp.Web.Entity
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImageUrl { get; set; }

        public Person Person { get; set; }
    }
}