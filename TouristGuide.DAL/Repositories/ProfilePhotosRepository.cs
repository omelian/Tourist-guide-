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
    public class ProfilePhotosRepository : IRepository<ProfilePhoto>
    {
        /// <summary>
        /// Reference to database
        /// </summary>
        private ApplicationContext dataBase;

        /// <summary>
        /// Initializes a new instance of the ProfilePhotosRepository class.
        /// </summary>
        /// <param name="db">Reference to data base</param>
        public ProfilePhotosRepository(ApplicationContext db)
        {
            this.dataBase = db;
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Create new ProfilePhoto item
        /// </summary>
        /// <param name="item">Item to create</param>
        public void Create(ProfilePhoto item)
        {
            this.dataBase.ProfilePhotos.Add(item);
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Get ProfilePhoto item by id
        /// </summary>
        /// <param name="id">Id of ProfilePhoto item</param>
        /// <returns>ProfilePhoto item</returns>
        public ProfilePhoto Get(object id)
        {
            return this.dataBase.ProfilePhotos.Find(id);
        }

        /// <summary>
        /// Get collection of all ProfilePhoto items 
        /// </summary>
        /// <returns>Collection of ProfilePhoto</returns>
        public IEnumerable<ProfilePhoto> GetAll()
        {
            return this.dataBase.ProfilePhotos;
        }

        /// <summary>
        /// Find ProfilePhoto item by predicate
        /// </summary>
        /// <param name="predicate">Predicate to search</param>
        /// <returns> Collection of ProfilePhotos </returns>
        public IEnumerable<ProfilePhoto> Find(Func<ProfilePhoto, bool> predicate)
        {
            return this.dataBase.ProfilePhotos.Where(predicate).ToList();
        }

        /// <summary>
        /// Update ProfilePhoto item
        /// </summary>
        /// <param name="item">Item to update</param>
        public void Update(ProfilePhoto item)
        {
            this.dataBase.Entry(item).State = EntityState.Modified;
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Delete ProfilePhoto item by id
        /// </summary>
        /// <param name="id">Id of item to delete</param>
        public void Delete(object id)
        {
            ProfilePhoto profilePhoto = this.dataBase.ProfilePhotos.Find(id);
            if (profilePhoto != null)
            {
                this.dataBase.ProfilePhotos.Remove(profilePhoto);
                this.dataBase.SaveChanges();
            }
        }
    }
}
