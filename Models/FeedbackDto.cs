using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noticia.Models
{
    public class FeedbackDto
    {
        public int PostId { get; set; }
        public string Sentimiento { get; set; }
    }
}