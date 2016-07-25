using System;
using System.Collections.Generic;
using System.Linq;
using TouristGuide.DAL.Interfaces;
using TouristGuide.INF.EntityFramework;
using TouristGuide.INF.Models;

namespace TouristGuide.DAL.Repositories
{
    /// <summary>
    /// Repository for articles
    /// </summary>
    public  class ArticleRepository : IRepository<Article>
    {
        /// <summary>
        /// Reference to database
        /// </summary>
        private ApplicationContext dataBase;

        /// <summary>
        /// Initializes a new instance of the ArticleRepository class.
        /// </summary>
        /// <param name="db">Reference to data base</param>
        public ArticleRepository(ApplicationContext db)
        {
            this.dataBase = db;
        }

        /// <summary>
        /// Create new Article item
        /// </summary>
        /// <param name="item">Item to create</param>
        public void Create(Article item)
        {
            this.dataBase.Articles.Add(item);
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Get Article item by id
        /// </summary>
        /// <param name="id">Id of Article item</param>
        /// <returns>Article item</returns>
        public Article Get(object id)
        {
            return this.dataBase.Articles.Find(id);
        }

        /// <summary>
        /// Get collection of all Article items 
        /// </summary>
        /// <returns>Collection of Articles</returns>
        public IEnumerable<Article> GetAll()
        {
            return this.dataBase.Articles;
        }

        /// <summary>
        /// Find Article items by predicate
        /// </summary>
        /// <param name="predicate">Predicate to search</param>
        /// <returns> Collection of Articles </returns>
        public IEnumerable<Article> Find(Func<Article, bool> predicate)
        {
            return this.dataBase.Articles.Where(predicate).ToList();
        }

        /// <summary>
        /// Update Article item
        /// </summary>
        /// <param name="item">Item to update</param>
        public void Update(Article item)
        {
            var result = this.dataBase.Articles.SingleOrDefault(b => b.ArticleId == item.ArticleId);
            if (result != null)
            {
                result.Title = item.Title;
                result.Text = item.Text;
                result.PictureUrl = item.PictureUrl;
                result.Email = item.Email;
                result.PhoneNumber = item.PhoneNumber;
                result.FacebookReference = item.FacebookReference;
                result.WebSiteReference = item.WebSiteReference;
            }

            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Delete Comment item by id
        /// </summary>
        /// <param name="id">Id of item to delete</param>
        public void Delete(object id)
        {
            Article article = this.dataBase.Articles.Find(id);
            if (article != null)
            {
                this.dataBase.Articles.Remove(article);
                this.dataBase.SaveChanges();
            }
        }
    }
}
