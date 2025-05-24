using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using noticia.Data;
using noticia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace noticia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Solo usuarios autenticados pueden dar feedback
    public class FeedbackApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public FeedbackApiController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // POST: api/feedback
        [HttpPost]
        public async Task<IActionResult> PostFeedback([FromBody] FeedbackDto dto)
        {
            var userId = _userManager.GetUserId(User);

            // Validar que no exista feedback previo del usuario para el mismo post
            var existe = _context.Feedbacks.Any(f => f.PostId == dto.PostId && f.UserId == userId);
            if (existe)
                return BadRequest("Ya has votado en este post.");

            var feedback = new Feedback
            {
                PostId = dto.PostId,
                Sentimiento = dto.Sentimiento,
                Fecha = DateTime.UtcNow,
                UserId = userId
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return Ok(feedback);
        }

        // GET: api/feedback
        [HttpGet]
        public IActionResult GetFeedbacks()
        {
            return Ok(_context.Feedbacks.ToList());
        }
    }
}