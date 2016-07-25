using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristGuide.DAL.Interfaces;
using TouristGuide.INF.EntityFramework;
using TouristGuide.INF.Identity;
using TouristGuide.INF.Models;

namespace TouristGuide.DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;

        public IdentityUnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public IRepository<Address> AddressManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<BannedProfile> BannedProfileManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<BannedUser> BannedUserManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<Comment> CommentManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<Company> CompanyManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<Location> LocationManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<Profile> ProfileManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<ProfileType> ProfileTypeManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<Rate> RateManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
