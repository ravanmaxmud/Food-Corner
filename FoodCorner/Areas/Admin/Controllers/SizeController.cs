using FoodCorner.Areas.Admin.ViewModels.Size;
using FoodCorner.Contracts.File;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FoodCorner.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/size")]
    [Authorize(Roles = "admin")]
    public class SizeController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<CategoryController> _logger;
        private readonly IFileService _fileService;


        public SizeController(DataContext dataContext, ILogger<CategoryController> logger, IFileService fileService)
        {
            _dataContext = dataContext;
            _logger = logger;
            _fileService = fileService;
        }

        [HttpGet("list", Name = "admin-size-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Sizes.Select(c => new ListItemViewModel(c.Id, c.PersonSize, c.IncreasePercent)).ToListAsync();
            return View(model);
        }

        [HttpGet("add", Name = "admin-size-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost("add", Name = "admin-size-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var size = new Size
            {
                PersonSize = model.PersonSize,
                IncreasePercent = model.IncreasePercent
            };
            await _dataContext.Sizes.AddAsync(size);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-size-list");
        }


        [HttpGet("update/{id}", Name = "admin-size-update")]
        public async Task<IActionResult> Update([FromRoute] int id)
        {
            var size = await _dataContext.Sizes.FirstOrDefaultAsync(s=> s.Id == id);
            if (size is null)
            {
                return NotFound();
            }
            var model = new UpdateViewModel 
            {
                Id = size.Id,
               PersonSize = size.PersonSize,
               IncreasePercent = size.IncreasePercent
            };

            return View(model);
        }
        [HttpPost("update/{id}", Name = "admin-size-update")]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            var size = await _dataContext.Sizes.FirstOrDefaultAsync(s => s.Id == model.Id);
            if (size is null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            size.PersonSize = model.PersonSize;
            size.IncreasePercent = model.IncreasePercent; 

            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-size-list");
        }


        [HttpPost("delete/{id}", Name = "admin-size-delete")]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            var size = await _dataContext.Sizes.FirstOrDefaultAsync(c => c.Id == id);

            if (size is null)
            {
                return NotFound();
            }    
            _dataContext.Sizes.Remove(size);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-size-list");
        }
    }
}
