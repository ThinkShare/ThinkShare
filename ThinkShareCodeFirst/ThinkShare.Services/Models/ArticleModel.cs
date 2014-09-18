namespace ThinkShare.Services.Models
{
    using System;
    using System.Collections.Generic;

    public class ArticleModel
    {
        public int? ArticleId { get; set; }

        public string ArticleHead { get; set; }

        public string ArticleAuthor { get; set; }

        public string ArticleText { get; set; }

        public DateTime Date { get; set; }

        public string Password { get; set; }

        public int Category { get; set; }

        public string ArticleCategory { get; set; }

        public ICollection<CommentModel> Comments { get; set; }
    }
}