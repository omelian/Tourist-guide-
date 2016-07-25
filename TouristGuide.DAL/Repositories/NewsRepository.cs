using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TouristGuide.DAL.Interfaces;
using TouristGuide.INF.EntityFramework;
using TouristGuide.INF.Models;

namespace TouristGuide.DAL.Repositories
{
    /// <summary>
    /// Repository for addresses
    /// </summary>
    public class NewsRepository : IRepository<News>
    {
        /// <summary>
        /// Reference to database
        /// </summary>
        private ApplicationContext dataBase;

        /// <summary>
        /// Initializes a new instance of the NewsRepository class.
        /// </summary>
        /// <param name="db">Reference to data base</param>
        public NewsRepository(ApplicationContext db)
        {
            this.dataBase = db;
        }

        /// <summary>
        /// Create new News item
        /// </summary>
        /// <param name="item">Item to create</param>
        public void Create(News item)
        {
            this.dataBase.ProfileNews.Add(item);
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Get News item by id
        /// </summary>
        /// <param name="id">Id of News item</param>
        /// <returns>News item</returns>
        public News Get(object id)
        {
            return this.dataBase.ProfileNews.Find(id);
        }

        /// <summary>
        /// Get collection of all News items 
        /// </summary>
        /// <returns>Collection of News</returns>
        public IEnumerable<News> GetAll()
        {
            return this.dataBase.ProfileNews;
        }

        /// <summary>
        /// Find News item by predicate
        /// </summary>
        /// <param name="predicate">Predicate to search</param>
        /// <returns> Collection of News </returns>
        public IEnumerable<News> Find(Func<News, bool> predicate)
        {
            return this.dataBase.ProfileNews.Where(predicate).ToList();
        }

        /// <summary>
        /// Update News item
        /// </summary>
        /// <param name="item">Item to update</param>
        public void Update(News item)
        {
            var result = this.dataBase.ProfileNews.SingleOrDefault(b => b.NewsId == item.NewsId);
            if (result != null)
            {
                result.Title = item.Title;
                result.TextContent = item.TextContent;
                result.NewsDate = item.NewsDate;
                result.NewsImageUrl = item.NewsImageUrl;
            }

            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Delete News item by id
        /// </summary>
        /// <param name="id">Id of item to delete</param>
        public void Delete(object id)
        {
            News news = this.dataBase.ProfileNews.Find(id);
            if (news != null)
            {
                this.dataBase.ProfileNews.Remove(news);
                this.dataBase.SaveChanges();
            }
        }
    }
}
