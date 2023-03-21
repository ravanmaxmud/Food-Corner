using FoodCorner.Areas.Admin.ViewModels.About;
using FoodCorner.Contracts.File;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/about")]
    [Authorize(Roles = "admin")]
    public class AboutController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _provider;
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public AboutController(DataContext dataContext, IFileService fileService, IActionDescriptorCollectionProvider provider)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _provider = provider;
        }

        [HttpGet("TeamList", Name = "admin-team-list")]
        public async Task<IActionResult> TeamList()
        {
            var model = await _dataContext.TeamMembers.Select(t =>
            new TeamListViewModel(t.Id, $"{t.Name} {t.LastName}", t.InistagramUrl!, t.LinkEdinUrl!, t.FaceBookUrl!
            , _fileService.GetFileUrl(t.MemberİmageInFileSystem, Contracts.File.UploadDirectory.TeamMembers))).ToListAsync();

            return View(model);
        }
        [HttpGet("TeamAdd", Name = "admin-team-add")]
        public async Task<IActionResult> TeamAdd()
        {
            return View();
        }
        [HttpPost("TeamAdd", Name = "admin-team-add")]
        public async Task<IActionResult> TeamAdd(TeamAddViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }


            var imageNameInSystem = await _fileService.UploadAsync(model.MembersImage, UploadDirectory.TeamMembers);

            AddMembers(model.MembersImage.FileName, imageNameInSystem);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-team-list");

            async void AddMembers(string imageName, string imageNameInSystem)
            {
                var team = new TeamMember
                {
                    Name = model.Name,
                    LastName = model.LastName,
                    InistagramUrl = model.Instagram,
                    LinkEdinUrl = model.LinkEdin,
                    FaceBookUrl = model.Facebook,
                    MemberImage = imageName,
                    MemberİmageInFileSystem = imageNameInSystem,
                    CreatedAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                };

                await _dataContext.TeamMembers.AddAsync(team);
            }
        }
        [HttpPost("delete/{id}", Name = "admin-team-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var team = await _dataContext.TeamMembers.FirstOrDefaultAsync(s => s.Id == id);
            if (team is null)
            {
                return NotFound();
            }
            await _fileService.DeleteAsync(team.MemberİmageInFileSystem, UploadDirectory.TeamMembers);
            _dataContext.TeamMembers.Remove(team);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-team-list");
        }
    }
}
