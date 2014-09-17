namespace ThinkShare.Services.Models
{
    using System;

    public class ArticlesInputModel
    {
        public string ArticleHead { get; set; }

        public string ArticleAuthor { get; set; }

        public string ArticleText { get; set; }

        public DateTime Date { get; set; }

        public string Password { get; set; }

        public int Category { get; set; }
    }
}