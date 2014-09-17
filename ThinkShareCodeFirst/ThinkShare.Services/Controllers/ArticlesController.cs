namespace ThinkShare.Services.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Description;
    using System.Web.Http.Cors;
    using ThinkShare.Data;
    using ThinkShare.Model;
    using ThinkShare.Services.Models;
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ArticlesController : ApiController
    {
        private ThinkShareDbContext db = new ThinkShareDbContext();

        // GET: api/Articles
        public IQueryable<Object> GetArticles()
        {
            return db.Articles.Select(x => new
            {
                articleId = x.Id,
                articleHead = x.Heading,
                articleAuthor = x.Author,
                articleCategory = x.Category.PictureUrl
            });
        }

        // GET: api/Articles/5
        [ResponseType(typeof(Article))]
        public IHttpActionResult GetArticle(int id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                articleId = article.Id,
                articleHead = article.Heading,
                articleAuthor = article.Author,
                articleText = article.Text,
                date = article.Date,
                category = article.Category.Id,
                comments = article.Comments.Select(x => new
                {
                    author = x.Author,
                    text = x.Text,
                    date = x.Date
                })
            });
        }

        // GET: api/Articles/Pesho
        [ResponseType(typeof(Article))]
        public IHttpActionResult GetArticlesByAuthorName(string name)
        {
            var articles = db.Articles.Where(x => x.Author == name);
            if (articles.Count() == 0)
            {
                return NotFound();
            }

            return Ok(articles.Select(x=>new
            {
                articleId = x.Id,
                articleHead = x.Heading,
                articleAuthor = x.Author,
                articleText = x.Text,
                date = x.Date,
                category = x.Category.Id,
                comments = x.Comments.Select(y => new
                {
                    author = y.Author,
                    text = y.Text,
                    date = y.Date
                })
            }));
        }

        // GET: api/Articles/Categories/3
        [ResponseType(typeof(Article))]
        public IHttpActionResult GetArticlesByCategoryId(int id)
        {
            var articles = db.Articles.Where(x => x.Category.Id == id);
            if (articles.Count() == 0)
            {
                return NotFound();
            }

            return Ok(articles.Select(x => new
            {
                articleId = x.Id,
                articleHead = x.Heading,
                articleAuthor = x.Author,
                articleText = x.Text,
                date = x.Date,
                category = x.Category.Id,
                comments = x.Comments.Select(y => new
                {
                    author = y.Author,
                    text = y.Text,
                    date = y.Date
                })
            }));
        }

        // GET: api/Articles/Pesho
        [ResponseType(typeof(Article))]
        public IHttpActionResult GetArticlesByTag(Tag tag)
        {
            var articles = db.Articles.Where(x => x.Tags.Contains(tag));

            if (articles.Count() == 0)
            {
                return NotFound();
            }

            return Ok(articles.Select(x => new
            {
                articleId = x.Id,
                articleHead = x.Heading,
                articleAuthor = x.Author,
                articleText = x.Text,
                date = x.Date,
                category = x.Category.Id,
                comments = x.Comments.Select(y => new
                {
                    author = y.Author,
                    text = y.Text,
                    date= y.Date
                })
            }));
        }

        // PUT: api/Articles/PutArticle/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArticle(int id, ArticlesInputModel article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingArticle = this.db.Articles.FirstOrDefault(a => a.Id == id);

            if (existingArticle == null)
            {
                return BadRequest("Such article does not exists!");
            }
            else if (existingArticle.Password != article.Password)
            {
                return BadRequest("Invalid password!");
            }

            existingArticle.Heading = article.ArticleHead;
            existingArticle.Text = article.ArticleText;
            existingArticle.Date = article.Date;
            existingArticle.Author = article.ArticleAuthor;
            existingArticle.CategoryId = article.Category;

            this.db.SaveChanges();

            return Ok();
            //return Ok(existingArticle);
        }

        // POST: api/Articles/PostArticle
        [ResponseType(typeof(ArticlesInputModel))]
        public IHttpActionResult PostArticle(ArticlesInputModel article)
        {
            if (article == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newArticle = new Article
            {
                Heading = article.ArticleHead,
                Author = article.ArticleAuthor,
                Text = article.ArticleText,
                Date = article.Date,
                CategoryId = article.Category,
                Password = article.Password
            };

            db.Articles.Add(newArticle);
            db.SaveChanges();

            return Ok(newArticle);
        }

        // DELETE: api/Articles/5
        [ResponseType(typeof(Article))]
        public IHttpActionResult DeleteArticle(int id, string password)
        {
            var existingArticle = this.db.Articles.FirstOrDefault(a => a.Id == id);

            if (existingArticle == null)
            {
                return BadRequest("Such aircraft does not exists!");
            }

            else if (existingArticle.Password != password)
            {
                return BadRequest("Invalid password!");
            }

            this.db.Articles.Remove(existingArticle);
            this.db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticleExists(int id)
        {
            return db.Articles.Count(e => e.Id == id) > 0;
        }
    }
}