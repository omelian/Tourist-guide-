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
    /// Repository for profiles
    /// </summary>
    public class ProfileRepository : IRepository<Profile>
    {
        /// <summary>
        /// Reference to database
        /// </summary>
        private ApplicationContext dataBase;

        /// <summary>
        /// Initializes a new instance of the ProfileRepository class.
        /// </summary>
        /// <param name="db">Reference to data base</param>
        public ProfileRepository(ApplicationContext db)
        {
            this.dataBase = db;
        }

        /// <summary>
        /// Create new Profile item
        /// </summary>
        /// <param name="item">Item to create</param>
        public void Create(Profile item)
        {
            this.dataBase.Profiles.Add(item);
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Get Profile item by id
        /// </summary>
        /// <param name="id">Id of Profile item</param>
        /// <returns>Profile item</returns>
        public Profile Get(object id)
        {
            return this.dataBase.Profiles.Find(id);
        }

        /// <summary>
        /// Get collection of all Profiles items 
        /// </summary>
        /// <returns>Collection of Profiles</returns>
        public IEnumerable<Profile> GetAll()
        {
            return this.dataBase.Profiles;
        }

        /// <summary>
        /// Find Profile items by predicate
        /// </summary>
        /// <param name="predicate">Predicate to search</param>
        /// <returns> Collection of Profiles </returns>
        public IEnumerable<Profile> Find(Func<Profile, bool> predicate)
        {
            return this.dataBase.Profiles.Where(predicate).ToList();
        }

        /// <summary>
        /// Update Profile item
        /// </summary>
        /// <param name="item">Item to update</param>
        public void Update(Profile item)
        {
            var result = this.dataBase.Profiles.SingleOrDefault(b => b.ProfileId == item.ProfileId);
            if (result != null)
            {
                result.Moders = item.Moders;
                result.Comments = item.Comments;
                result.News = item.News;
                result.MainPhoto = item.MainPhoto;
                result.Address = item.Address;
                result.IsDeleted = item.IsDeleted;
            }
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Delete Profile item by id
        /// </summary>
        /// <param name="id">Id of item to delete</param>
        public void Delete(object id)
        {
            Profile profile = this.dataBase.Profiles.Find(id);
            if (profile != null)
            {
                this.dataBase.Profiles.Remove(profile);
                this.dataBase.SaveChanges();
            }
        }
    }
}
