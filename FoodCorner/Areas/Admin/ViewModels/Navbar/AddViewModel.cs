﻿using Microsoft.Build.Framework;

namespace FoodCorner.Areas.Admin.ViewModels.Navbar
{
    public class AddViewModel
    {
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


        public class UrlViewModel
        {
            public UrlViewModel(string? routName,string? path)
            {
                RoutName = routName;
                Path = path;
            }
            public UrlViewModel()
            {

            }

            public string? RoutName { get; set; }
            public string? Path { get; set; }
        }
    }
}
