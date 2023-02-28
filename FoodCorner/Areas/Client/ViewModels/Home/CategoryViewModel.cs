﻿namespace FoodCorner.Areas.Client.ViewModels.Home
{
    public class CategoryViewModel
    {
        public CategoryViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
}
