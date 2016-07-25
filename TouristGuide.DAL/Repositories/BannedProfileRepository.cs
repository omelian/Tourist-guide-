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
    /// Repository for banned profiles
    /// </summary>
    public class BannedProfileRepository : IRepository<BannedProfile>
    {
        /// <summary>
        /// Reference to database
        /// </summary>
         private ApplicationContext dataBase;

        /// <summary>
         /// Initializes a new instance of the BannedProfileRepository class.
        /// </summary>
        /// <param name="db">Reference to data base</param>
        public BannedProfileRepository(ApplicationContext db)
        {
            this.dataBase = db;
        }

        /// <summary>
        /// Create new BannedProfile item
        /// </summary>
        /// <param name="item">Item to create</param>
        public void Create(BannedProfile item)
        {
            this.dataBase.BannedProfiles.Add(item);
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Get BannedProfile item by id
        /// </summary>
        /// <param name="id">Id of BannedProfile item</param>
        /// <returns>BannedProfile item</returns>
        public BannedProfile Get(object id)
        {
            return this.dataBase.BannedProfiles.Find(id);
        }

        /// <summary>
        /// Get collection of all BannedProfile items 
        /// </summary>
        /// <returns>Collection of BannedProfiles</returns>
        public IEnumerable<BannedProfile> GetAll()
        {
            return this.dataBase.BannedProfiles;
        }

        /// <summary>
        /// Find BannedProfile item by predicate
        /// </summary>
        /// <param name="predicate">Predicate to search</param>
        /// <returns> Collection of BannedProfiles </returns>
        public IEnumerable<BannedProfile> Find(Func<BannedProfile, bool> predicate)
        {
            return this.dataBase.BannedProfiles.Where(predicate).ToList();
        }

        /// <summary>
        /// Update BannedProfile item
        /// </summary>
        /// <param name="item">Item to update</param>
        public void Update(BannedProfile item)
        {
            this.dataBase.Entry(item).State = EntityState.Modified;
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Delete BannedProfile item by id
        /// </summary>
        /// <param name="id">Id of item to delete</param>
        public void Delete(object id)
        {
            BannedProfile bannedProfile = this.dataBase.BannedProfiles.Find(id);
            if (bannedProfile != null)
            {
                this.dataBase.BannedProfiles.Remove(bannedProfile);
                this.dataBase.SaveChanges();
            }   
        }
    }
}
