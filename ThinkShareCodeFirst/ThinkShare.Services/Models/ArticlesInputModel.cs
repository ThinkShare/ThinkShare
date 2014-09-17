namespace ThinkShare.Services.Models
{
    using System;

    public class ArticlesInputModel
    {
        public string Heading { get; set; }

        public string Author { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string Password { get; set; }

        public int CategoryId { get; set; }
    }
}