using ForumApp.Data;
using ForumApp.Interfaces;
using ForumApp.Models;
using ForumApp.Repository;
using ForumApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading;

namespace ForumApp.Controllers
{
    public class ThreadController : Controller
    {
        private readonly IThreadRepository _threadRepository;
        public ThreadController(IThreadRepository threadRepository)
        {
            _threadRepository = threadRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Models.Thread> threads = await _threadRepository.GetAll();
            return View(threads);
        }

        public async Task<IActionResult> Details(int id)
        {
            Models.Thread thread = await _threadRepository.GetByIdAsync(id);
            return View(thread);
        }

        [HttpGet]
        public IActionResult Create(int forumId)
        {
            var curUserId = HttpContext.User.GetUserId();
            var createThreadViewModel = new CreateThreadViewModel { ForumId = forumId, AuthorId = curUserId };
            
            return View(createThreadViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateThreadViewModel threadVM)
        {
            if (ModelState.IsValid)
            {
                var thread = new Models.Thread
                {
                    Title = threadVM.Title,
                    Content = threadVM.Content,
                    CreationDate = DateTime.Now,
                    ForumId = threadVM.ForumId,
                    AuthorId = threadVM.AuthorId
                };
                _threadRepository.Add(thread);
                return RedirectToAction("Detail", "Forum", new {id = threadVM.ForumId});
            }

            return View(threadVM);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> PinThread(int id)
        {
            Models.Thread thread = await _threadRepository.GetByIdAsync(id);
            if (thread == null) return View("Error");
            var threadVM = new PinThreadViewModel
            {
                Pinned = thread.Pinned,
                ForumId = thread.ForumId
            };
            return View(threadVM);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> PinThread(int id, PinThreadViewModel pinThreadVM)
        {
            _threadRepository.Pinned(id, !pinThreadVM.Pinned);

            return RedirectToAction("Detail", "Forum", new { id = pinThreadVM.ForumId });
        }

    }
}
