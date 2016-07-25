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
    /// Repository for banned users
    /// </summary>
    public class BannedUserRepository : IRepository<BannedUser>
    {
        /// <summary>
        /// Reference to database
        /// </summary>
        private ApplicationContext dataBase;

        /// <summary>
        /// Initializes a new instance of the BannedUserRepository class.
        /// </summary>
        /// <param name="db">Reference to data base</param>
        public BannedUserRepository(ApplicationContext db)
        {
            this.dataBase = db;
        }

        /// <summary>
        /// Create new BannedUser item
        /// </summary>
        /// <param name="item">Item to create</param>
        public void Create(BannedUser item)
        {
            this.dataBase.BannedUsers.Add(item);
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Get BannedUser item by id
        /// </summary>
        /// <param name="id">Id of BannedUser item</param>
        /// <returns>BannedUser item</returns>
        public BannedUser Get(object id)
        {
            return this.dataBase.BannedUsers.Find(id);
        }

        /// <summary>
        /// Get collection of all BannedUser items 
        /// </summary>
        /// <returns>Collection of BannedUsers</returns>
        public IEnumerable<BannedUser> GetAll()
        {
            return this.dataBase.BannedUsers;
        }

        /// <summary>
        /// Find BannedUsers item by predicate
        /// </summary>
        /// <param name="predicate">Predicate to search</param>
        /// <returns> Collection of BannedUsers </returns>
        public IEnumerable<BannedUser> Find(Func<BannedUser, bool> predicate)
        {
            return this.dataBase.BannedUsers.Where(predicate).ToList();
        }

        /// <summary>
        /// Update BannedUser item
        /// </summary>
        /// <param name="item">Item to update</param>
        public void Update(BannedUser item)
        {
            this.dataBase.Entry(item).State = EntityState.Modified;
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Delete BannedUser item by id
        /// </summary>
        /// <param name="id">Id of item to delete</param>
        public void Delete(object id)
        {
            BannedUser banneduser = this.dataBase.BannedUsers.Find(id);
            if (banneduser != null)
            {
                this.dataBase.BannedUsers.Remove(banneduser);
                this.dataBase.SaveChanges();
            }
        }
    }
}
