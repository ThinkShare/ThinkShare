using System;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThinkShare.Data;
using ThinkShare.Data.Repositories;
using ThinkShare.Model;
using System.Data.Entity;

namespace ThinkShare.Repositories.Tests
{
    [TestClass]
    public class DbRepositoryTests
    {
        public DbContext dbContext { get; set; }

        static Random rand = new Random();

        public IRepository<Category> categoriesRepository { get; set; }

        private static TransactionScope tranScope;

        public DbRepositoryTests()
        {
            this.dbContext = new ThinkShareDbContext();
            this.categoriesRepository = new Repository<Category>();
        }

        [TestInitialize]
        public void TestInit()
        {
            tranScope = new TransactionScope();
        }

        [TestCleanup]
        public void TestTearDown()
        {
            tranScope.Dispose();
        }

        [TestMethod]
        public void Add_ValidCategory_ShouldCategoryIdBiggerThanNull()
        {
            var category = new Category
            {
                PictureUrl = "http://www.test.com/",
                Title = "Test"
            };
            this.dbContext.Set<Category>().Add(category);
            this.dbContext.SaveChanges();
            Assert.IsTrue(category.Id > 0);
        }

        [TestMethod]
        public void Add_ValidArticle_ShouldCategoryIdBiggerThanNull()
        {
            var article = new Article
            {
                Author = "Pesho",
                Category = new Category
                {
                    PictureUrl = "http://www.test.com/",
                    Title = "Test"
                },
                Password = "TestPass",
                Date = DateTime.Now
            };

            this.dbContext.Set<Article>().Add(article);
            dbContext.SaveChanges();
            Assert.IsTrue(article.Id > 0);
        }

        [TestMethod]
        public void Add_WhenCategoryIsValid_ShouldReturnNotZeroId()
        {
            int catId;
            using (TransactionScope scope = new TransactionScope())
            {
                var category = new Category
                {
                    PictureUrl = "http://www.test.com/",
                    Title = "Test"
                };
                this.dbContext.Set<Category>().Add(category);
                this.dbContext.SaveChanges();
                scope.Complete();
                catId = category.Id;
            }
            Assert.IsTrue(catId != 0);
            var catEntity = this.dbContext.Set<Category>().Find(catId);
            Assert.IsNotNull(catEntity);
        }

        [TestMethod]
        public void Add_WhenArticleIsValid_ShouldReturnNotZeroId()
        {
            int articleId;
            using (TransactionScope scope = new TransactionScope())
            {
                var article = new Article
                {
                    Author = "Pesho",
                    Category = new Category
                    {
                        PictureUrl = "http://www.test.com/",
                        Title = "Test"
                    },
                    Password = "TestPass",
                    Date = DateTime.Now
                };
                this.dbContext.Set<Article>().Add(article);
                this.dbContext.SaveChanges();
                scope.Complete();
                articleId = article.Id;
            }
            Assert.IsTrue(articleId != 0);
            var articleEntity = this.dbContext.Set<Article>().Find(articleId);
            Assert.IsNotNull(articleEntity);
        }

        //[TestMethod]
        //public void Add_WhenNameIsValid_ShouldAddCategoryToDatabase()
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //    var categoryName = "Test category";
        //    var category = new Category
        //    {
        //        PictureUrl = "http://www.test.com/",
        //        Title = "Test"
        //    };

        //        var createdCategory = this.categoriesRepository.Add(category);
        //    var foundCategory = this.dbContext.Set<Category>().Find(createdCategory.Id);
        //    Assert.IsNotNull(foundCategory);
        //    Assert.AreEqual(categoryName, foundCategory.Name);
        //    }
        //}

        //[TestMethod]
        //public void Add_WhenNameIsValid_ShouldReturnNotZeroId()
        //{
        //    var category = new Category()
        //    {
        //        PictureUrl = "http://www.test.com/",
        //        Title = "Test",
        //        Id = Category.Id
        //    };

        //    var createdCategory = this.dbContext.Set<Category>().Add(category);
        //    var createdCategoryId = createdCategory.
        //    Assert.IsTrue(createdCategory.Id != 0);
        //}
    }
}
