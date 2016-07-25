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
    /// Repository for comments
    /// </summary>
    public class CommentRepository : IRepository<Comment>
    {
        /// <summary>
        /// Reference to database
        /// </summary>
        private ApplicationContext dataBase;

        /// <summary>
        /// Initializes a new instance of the CommentRepository class.
        /// </summary>
        /// <param name="db">Reference to data base</param>
        public CommentRepository(ApplicationContext db)
        {
            this.dataBase = db;
        }

        /// <summary>
        /// Create new Comment item
        /// </summary>
        /// <param name="item">Item to create</param>
        public virtual void Create(Comment item)
        {
            this.dataBase.Comments.Add(item);
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Get Comment item by id
        /// </summary>
        /// <param name="id">Id of Comment item</param>
        /// <returns>Comment item</returns>
        public virtual Comment Get(object id)
        {
            return this.dataBase.Comments.Find(id);
        }

        /// <summary>
        /// Get collection of all Comment items 
        /// </summary>
        /// <returns>Collection of Comments</returns>
        public virtual IEnumerable<Comment> GetAll()
        {
            return this.dataBase.Comments;
        }

        /// <summary>
        /// Find Comment items by predicate
        /// </summary>
        /// <param name="predicate">Predicate to search</param>
        /// <returns> Collection of Comments </returns>
        public virtual IEnumerable<Comment> Find(Func<Comment, bool> predicate)
        {
            return this.dataBase.Comments.Where(predicate).ToList();
        }

        /// <summary>
        /// Update Comment item
        /// </summary>
        /// <param name="item">Item to update</param>
        public virtual void Update(Comment item)
        {
            var result = this.dataBase.Comments.SingleOrDefault(b => b.CommentId == item.CommentId);
            if (result != null)
            {
                result.CommentDateTime = item.CommentDateTime;
                result.Text = item.Text;
            }

            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Delete Comment item by id
        /// </summary>
        /// <param name="id">Id of item to delete</param>
        public virtual void Delete(object id)
        {
            Comment comment = this.dataBase.Comments.Find(id);
            if (comment != null)
            {
                this.dataBase.Comments.Remove(comment);
                this.dataBase.SaveChanges();
            }
        }
    }
}
