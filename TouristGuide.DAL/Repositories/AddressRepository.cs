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
    public class AddressRepository : IRepository<Address>
    {
        /// <summary>
        /// Reference to database
        /// </summary>
        private ApplicationContext dataBase;

        /// <summary>
        /// Initializes a new instance of the AddressRepository class.
        /// </summary>
        /// <param name="db">Reference to data base</param>
        public AddressRepository(ApplicationContext db)
        {
            this.dataBase = db;
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Create new Address item
        /// </summary>
        /// <param name="item">Item to create</param>
        public void Create(Address item)
        {
            this.dataBase.Addresses.Add(item);
        }

        /// <summary>
        /// Get Address item by id
        /// </summary>
        /// <param name="id">Id of Address item</param>
        /// <returns>Address item</returns>
        public Address Get(object id)
        {
            return this.dataBase.Addresses.Find(id);
        }

        /// <summary>
        /// Get collection of all Address items 
        /// </summary>
        /// <returns>Collection of Addresses</returns>
        public IEnumerable<Address> GetAll()
        {
            return this.dataBase.Addresses;
        }

        /// <summary>
        /// Find Address item by predicate
        /// </summary>
        /// <param name="predicate">Predicate to search</param>
        /// <returns> Collection of Addresses </returns>
        public IEnumerable<Address> Find(Func<Address, bool> predicate)
        {
            return this.dataBase.Addresses.Where(predicate).ToList();
        }

        /// <summary>
        /// Update Address item
        /// </summary>
        /// <param name="item">Item to update</param>
        public void Update(Address item)
        {
            this.dataBase.Entry(item).State = EntityState.Modified;
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Delete Address item by id
        /// </summary>
        /// <param name="id">Id of item to delete</param>
        public void Delete(object id)
        {
            Address address = this.dataBase.Addresses.Find(id);
            if (address != null)
            {
                this.dataBase.Addresses.Remove(address);
                this.dataBase.SaveChanges();
            }   
        }
    }
}
