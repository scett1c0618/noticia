using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using noticia.Services;
using System.Threading.Tasks;
using noticia.Data; // Agrega este using

namespace noticia.Controllers
{
    public class PostsController : Controller
    {
        private readonly JsonPlaceholderService _jsonService;
        private readonly ApplicationDbContext _context; // Inyecta el contexto

        public PostsController(JsonPlaceholderService jsonService, ApplicationDbContext context)
        {
            _jsonService = jsonService;
            _context = context;
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

            // Obtener feedbacks
            var feedbacks = _context.Feedbacks.Where(f => f.PostId == id).ToList();
            var likes = feedbacks.Count(f => f.Sentimiento == "like");
            var dislikes = feedbacks.Count(f => f.Sentimiento == "dislike");

            ViewBag.Author = author;
            ViewBag.Comments = comments;
            ViewBag.Likes = likes;
            ViewBag.Dislikes = dislikes;

            return View(post);
        }
    }
}