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
                    text = x.Text
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
                category = x.Category.Title,
                comments = x.Comments.Select(y => new
                {
                    author = y.Author,
                    text = y.Text
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
                category = x.Category.Title,
                comments = x.Comments.Select(y => new
                {
                    author = y.Author,
                    text = y.Text
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
                category = x.Category.Title,
                comments = x.Comments.Select(y => new
                {
                    author = y.Author,
                    text = y.Text
                })
            }));
        }

        // PUT: api/Articles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArticle(int id, Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != article.Id)
            {
                return BadRequest();
            }

            db.Entry(article).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
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
                Heading = article.Heading,
                Author = article.Author,
                Text = article.Text,
                Date = article.Date,
                CategoryId = article.CategoryId,
                Password = article.Password
            };

            db.Articles.Add(newArticle);
            db.SaveChanges();

            return Ok(newArticle);
        }

        // DELETE: api/Articles/5
        [ResponseType(typeof(Article))]
        public IHttpActionResult DeleteArticle(int id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return NotFound();
            }

            db.Articles.Remove(article);
            db.SaveChanges();

            return Ok(article);
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