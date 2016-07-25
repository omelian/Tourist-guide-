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
    /// Repository for profile types
    /// </summary>
    public class ProfileTypeRepository : IRepository<ProfileType>
    {
        /// <summary>
        /// Reference to database
        /// </summary>
        private ApplicationContext dataBase;

        /// <summary>
        /// Initializes a new instance of the ProfileTypeRepository class.
        /// </summary>
        /// <param name="db">Reference to data base</param>
        public ProfileTypeRepository(ApplicationContext db)
        {
            this.dataBase = db;
        }

        /// <summary>
        /// Create new ProfileType item
        /// </summary>
        /// <param name="item">Item to create</param>
        public void Create(ProfileType item)
        {
            this.dataBase.ProfileTypes.Add(item);
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Get ProfileType item by id
        /// </summary>
        /// <param name="id">Id of ProfileType item</param>
        /// <returns>ProfileType item</returns>
        public ProfileType Get(object id)
        {
            return this.dataBase.ProfileTypes.Find(id);
        }

        /// <summary>
        /// Get collection of all ProfileType items 
        /// </summary>
        /// <returns>Collection of ProfileTypes</returns>
        public IEnumerable<ProfileType> GetAll()
        {
            return this.dataBase.ProfileTypes;
        }

        /// <summary>
        /// Find ProfileType items by predicate
        /// </summary>
        /// <param name="predicate">Predicate to search</param>
        /// <returns> Collection of ProfileTypes </returns>
        public IEnumerable<ProfileType> Find(Func<ProfileType, bool> predicate)
        {
            return this.dataBase.ProfileTypes.Where(predicate).ToList();
        }

        /// <summary>
        /// Update ProfileType item
        /// </summary>
        /// <param name="item">Item to update</param>
        public void Update(ProfileType item)
        {
            this.dataBase.Entry(item).State = EntityState.Modified;
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Delete ProfileType item by id
        /// </summary>
        /// <param name="id">Id of item to delete</param>
        public void Delete(object id)
        {
            ProfileType profileType = this.dataBase.ProfileTypes.Find(id);
            if (profileType != null)
            {
                this.dataBase.ProfileTypes.Remove(profileType);
                this.dataBase.SaveChanges();
            }
        }
    }
}
