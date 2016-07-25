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
    /// Repository for companies
    /// </summary>
    public class CompanyRepository : IRepository<Company>
    {
        /// <summary>
        /// Reference to database
        /// </summary>
        private ApplicationContext dataBase;

        /// <summary>
        /// Initializes a new instance of the CompanyRepository class.
        /// </summary>
        /// <param name="db">Reference to data base</param>
        public CompanyRepository(ApplicationContext db)
        {
            this.dataBase = db;
        }

        /// <summary>
        /// Create new Company item
        /// </summary>
        /// <param name="item">Item to create</param>
        public void Create(Company item)
        {
            this.dataBase.Companies.Add(item);
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Get Company item by id
        /// </summary>
        /// <param name="id">Id of Company item</param>
        /// <returns>Company item</returns>
        public Company Get(object id)
        {
            return this.dataBase.Companies.Find(id);
        }

        /// <summary>
        /// Get collection of all Companies items 
        /// </summary>
        /// <returns>Collection of Companies</returns>
        public IEnumerable<Company> GetAll()
        {
            return this.dataBase.Companies;
        }

        /// <summary>
        /// Find Company items by predicate
        /// </summary>
        /// <param name="predicate">Predicate to search</param>
        /// <returns> Collection of Companies </returns>
        public IEnumerable<Company> Find(Func<Company, bool> predicate)
        {
            return this.dataBase.Companies.Where(predicate).ToList();
        }

        /// <summary>
        /// Update Company item
        /// </summary>
        /// <param name="item">Item to update</param>
        public void Update(Company item)
        {
            this.dataBase.Entry(item).State = EntityState.Modified;
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Delete Company item by id
        /// </summary>
        /// <param name="id">Id of item to delete</param>
        public void Delete(object id)
        {
            Company company = this.dataBase.Companies.Find(id);
            if (company != null)
            {
                this.dataBase.Companies.Remove(company);
                this.dataBase.SaveChanges();
            }
        }
    }
}
