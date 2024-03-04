using ForumApp.Data;
using ForumApp.Data.Enum;
using ForumApp.Interfaces;
using ForumApp.Models;
using ForumApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace ForumApp.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumRepository _forumRepository;

        public ForumController(IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        } 

        public async Task<IActionResult> Index()
        {
            IEnumerable<IGrouping<ForumCategory, Forum>> forums = await _forumRepository.GetAllGroupedByCategory();
            return View(forums);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Forum forum = await _forumRepository.GetByIdAsync(id);
            return View(forum);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Create()
        {
            var curUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var createClubViewModel = new CreateForumViewModel();
            return View(createClubViewModel);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateForumViewModel forumVM)
        {
            if (ModelState.IsValid)
            {
                var forum = new Forum
                {
                    Title = forumVM.Title,
                    Description = forumVM.Description,
                    CreationDate = DateTime.Now,
                    ForumCategory = forumVM.ForumCategory,
                };
                _forumRepository.Add(forum);
                return RedirectToAction("Index");
            }

            return View(forumVM);
        }
    }
}
