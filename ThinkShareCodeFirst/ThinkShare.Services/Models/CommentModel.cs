namespace ThinkShare.Services.Models
{
    using System;

    public class CommentModel
    {
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string Author { get; set; }

        public int? ArticleId { get; set; }
    }
}