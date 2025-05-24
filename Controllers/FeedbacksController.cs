using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using noticia.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace noticia.Controllers
{
    public class FeedbacksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public FeedbacksController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Feedbacks
        public IActionResult Index()
        {
            var feedbacks = _context.Feedbacks.ToList();
            var users = _userManager.Users.ToList();

            var feedbackList = feedbacks.Select(f => new FeedbackViewModel
            {
                Id = f.Id,
                PostId = f.PostId,
                Sentimiento = f.Sentimiento,
                Fecha = f.Fecha,
                UserId = f.UserId,
                UserName = users.FirstOrDefault(u => u.Id == f.UserId)?.UserName ?? "Desconocido"
            }).ToList();

            return View(feedbackList);
        }
    }

    public class FeedbackViewModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Sentimiento { get; set; }
        public DateTime Fecha { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}