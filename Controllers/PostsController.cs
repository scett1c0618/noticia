using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using noticia.Services;
using System.Threading.Tasks;

namespace noticia.Controllers
{
    public class PostsController : Controller
    {
        private readonly JsonPlaceholderService _jsonService;

        public PostsController(JsonPlaceholderService jsonService)
        {
            _jsonService = jsonService;
        }

        // GET: /Posts
        public async Task<IActionResult> Index()
        {
            var posts = await _jsonService.GetPostsAsync();
            return View(posts);
        }

        // GET: /Posts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var post = await _jsonService.GetPostByIdAsync(id);
            if (post == null) return NotFound();

            var author = await _jsonService.GetUserByIdAsync(post.UserId);
            var comments = await _jsonService.GetCommentsByPostIdAsync(post.Id);

            ViewBag.Author = author;
            ViewBag.Comments = comments;

            return View(post);
        }
    }
}