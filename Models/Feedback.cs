using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noticia.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Sentimiento { get; set; } // "like" o "dislike"
        public DateTime Fecha { get; set; }
        public string UserId { get; set; } // Para asociar feedback a un usuario
    }
}