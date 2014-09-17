namespace ThinkShare.Services.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Description;
    using ThinkShare.Data;
    using ThinkShare.Model;
    using ThinkShare.Services.Models;

    public class CategoriesController : ApiController
    {
        private ThinkShareDbContext db = new ThinkShareDbContext();

        // GET: api/Categories/GetCategories
        public IQueryable<Object> GetCategories()
        {
            return db.Categories.Select(x => new CategoryModel
            {
                Id = x.Id,
                Title = x.Title,
                PictureUrl = x.PictureUrl
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.Id == id) > 0;
        }
    }
}