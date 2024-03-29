﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodCorner.Areas.Admin.ViewModels.Blog;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using FoodCorner.Services.Abstracts;
using FoodCorner.Contracts.File;
using FoodCorner.Migrations;

namespace FoodCorner.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/blog")]
    [Authorize(Roles = "admin")]
    public class BlogController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public readonly ILogger<BlogController> _logger;

        public BlogController(DataContext dataContext, IFileService fileService, ILogger<BlogController> logger)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _logger = logger;
        }

        [HttpGet("list",Name ="admin-blog-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Blogs.OrderByDescending(p => p.CreatedAt).Select(p => new ListItemViewModel(p.Id, p.Title,
                p.Content,
                p.BlogTags.Select(ps => ps.Tag).Select(s => new ListItemViewModel.TagViewModel(s.Title)).ToList(),
                p.BlogCategories.Select(pc => pc.Category).Select(c => new ListItemViewModel.CategoryViewModeL(c.Title, c.Parent.Title)).ToList(),
                p.CreatedAt
                )).ToListAsync();


            return View(model);
        }


        #region Add
        [HttpGet("add", Name = "admin-blog-add")]
        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel
            {
                Categorys = await _dataContext.BlogCategories
                    .Select(c => new BlogCategoryViewModel(c.Id, c.Title))
                    .ToListAsync(),
                Tags = await _dataContext.BlogTags.Select(t => new BlogTagViewModel(t.Id, t.Title)).ToListAsync()
            };

            return View(model);
        }

        [HttpPost("add", Name = "admin-blog-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            foreach (var categoryId in model.CategoryIds)
            {
                if (!await _dataContext.BlogCategories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({categoryId}) not found in db ");
                    return GetView(model);
                }

            }

            foreach (var tagId in model.TagIds)
            {
                if (!await _dataContext.BlogTags.AnyAsync(c => c.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Tag with id({tagId}) not found in db ");
                    return GetView(model);
                }

            }



            AddProduct();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blog-list");



            IActionResult GetView(AddViewModel model)
            {

                model.Categorys = _dataContext.BlogCategories
                   .Select(c => new BlogCategoryViewModel(c.Id, c.Title))
                   .ToList();

                model.Tags = _dataContext.BlogTags
                 .Select(c => new BlogTagViewModel(c.Id, c.Title))
                 .ToList();


                return View(model);
            }


            async void AddProduct()
            {
                var product = new Blog
                {
                 
                    Title = model.Title,
                    Content = model.Content,
                    CreatedAt = DateTime.Now,

                };

                await _dataContext.Blogs.AddAsync(product);

                foreach (var catagoryId in model.CategoryIds)
                {
                    var productCatagory = new BlogAndBlogCategory
                    {
                        BlogCategoryId = catagoryId,
                        Blog = product,
                    };

                    await _dataContext.BlogAndBlogCategories.AddAsync(productCatagory);
                }

                foreach (var tagId in model.TagIds)
                {
                    var productTag = new BlogAndBlogTag

                    {
                        BlogTagId = tagId,
                        Blog = product
                       
                    };

                    await _dataContext.BlogAndBlogTags.AddAsync(productTag);
                }


            }
        }

        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-blog-update")]
        public async Task<IActionResult> Update([FromRoute] int id)
        {
            var blog = await _dataContext.Blogs
                .Include(b=> b.BlogCategories)
                .Include(b=>b.BlogTags)
                .FirstOrDefaultAsync(b=> b.Id == id);
            if (blog == null) { return NotFound(); }

            var model = new UpdateViewModel
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                Categorys = await _dataContext.BlogCategories
                    .Select(c => new BlogCategoryViewModel(c.Id, c.Title))
                    .ToListAsync(),
                CategoryIds = blog.BlogCategories.Select(bc=> bc.BlogCategoryId).ToList(),
                Tags = await _dataContext.BlogTags.Select(t => new BlogTagViewModel(t.Id, t.Title)).ToListAsync(),
                TagIds = blog.BlogTags.Select(pc => pc.BlogTagId).ToList(),
            };


            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-blog-update")]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            var blog = await _dataContext.Blogs
                .Include(b=>b.BlogCategories).Include(b=> b.BlogTags)
                .FirstOrDefaultAsync(b=> b.Id == model.Id);

            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            foreach (var categoryId in model.CategoryIds)
            {
                if (!await _dataContext.BlogCategories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({categoryId}) not found in db ");
                    return GetView(model);
                }

            }

            foreach (var tagId in model.TagIds)
            {
                if (!await _dataContext.BlogTags.AnyAsync(c => c.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Tag with id({tagId}) not found in db ");
                    return GetView(model);
                }

            }

            UpdateBlog();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blog-list");



            IActionResult GetView(UpdateViewModel model)
            {

                model.Categorys = _dataContext.BlogCategories
                   .Select(c => new BlogCategoryViewModel(c.Id, c.Title))
                   .ToList();

                model.CategoryIds = blog.BlogCategories.Select(c => c.BlogCategoryId).ToList();

                model.Tags = _dataContext.BlogTags
                 .Select(c => new BlogTagViewModel(c.Id, c.Title))
                 .ToList();

                model.TagIds = blog.BlogTags.Select(c => c.BlogTagId).ToList(); 


                return View(model);
            }


            async void UpdateBlog()
            {
                blog.Title = model.Title;
                blog.Content = model.Content;
                blog.UpdateAt = DateTime.Now;


                #region Catagory
                var categoriesInDb = blog.BlogCategories.Select(bc => bc.BlogCategoryId).ToList();
                var categoriesToRemove = categoriesInDb.Except(model.CategoryIds).ToList();
                var categoriesToAdd = model.CategoryIds.Except(categoriesInDb).ToList();

                blog.BlogCategories.RemoveAll(bc => categoriesToRemove.Contains(bc.BlogCategoryId));

                foreach (var categoryId in categoriesToAdd)
                {
                    var blogCategory = new BlogAndBlogCategory
                    {
                        BlogCategoryId = categoryId,
                        Blog = blog,
                    };

                    await _dataContext.BlogAndBlogCategories.AddAsync(blogCategory);
                }
                #endregion
                #region Tag
                var tagsInDb = blog.BlogTags.Select(bc => bc.BlogTagId).ToList();
                var tagsToRemove = categoriesInDb.Except(model.TagIds).ToList();
                var tagsToAdd = model.TagIds.Except(categoriesInDb).ToList();

                blog.BlogTags.RemoveAll(bc => categoriesToRemove.Contains(bc.BlogTagId));

                foreach (var tagId in tagsToAdd)
                {
                    var blogTag = new BlogAndBlogTag
                    {
                        BlogTagId = tagId,
                        Blog = blog,
                    };

                    await _dataContext.BlogAndBlogTags.AddAsync(blogTag);
                }
                #endregion


            }
        }

        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-blog-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var products = await _dataContext.Blogs.FirstOrDefaultAsync(p => p.Id == id);

            if (products is null)
            {
                return NotFound();
            }

            _dataContext.Blogs.Remove(products);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-blog-list");
        }


        #endregion
    }
}
