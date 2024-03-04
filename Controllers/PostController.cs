using ForumApp.Interfaces;
using ForumApp.Models;
using ForumApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp;
using System.Security.Claims;

namespace ForumApp.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create(int threadId)
        {
            var curUserId = HttpContext.User.GetUserId();
            var createPostViewModel = new CreatePostViewModel { ThreadId = threadId, AuthorId = curUserId };

            return View(createPostViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel postVM)
        {
            if (ModelState.IsValid)
            {
                var post = new Models.Post
                {
                    Content = postVM.Content,
                    CreationDate = DateTime.Now,
                    ThreadId = postVM.ThreadId,
                    AuthorId = postVM.AuthorId
                };
                _postRepository.Add(post);
                return RedirectToAction("Details", "Thread", new { id = postVM.ThreadId });
            }

            return View(postVM);
        }
    }
}
