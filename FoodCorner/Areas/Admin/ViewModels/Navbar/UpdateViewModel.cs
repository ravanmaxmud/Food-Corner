﻿using System.ComponentModel.DataAnnotations;

namespace FoodCorner.Areas.Admin.ViewModels.Navbar
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ToURL { get; set; }

        public List<UrlViewModel>? Urls { get; set; }
        [Required]
        public int Order { get; set; }

        [Required]
        public bool IsViewHeader { get; set; }
        [Required]
        public bool IsViewFooter { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }


        public class UrlViewModel
        {
            public UrlViewModel(string? path)
            {
                Path = path;
            }

            public string? Path { get; set; }
        }
    }
}
