namespace ThinkShare.Services.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CommentModel
    {
        [MinLength(5)]
        [MaxLength(1500)]
        public string Text { get; set; }

        public DateTime Date { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        public string Author { get; set; }

        public int? ArticleId { get; set; }
    }
}