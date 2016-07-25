using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristGuide.BLL.DBContext;
using TouristGuide.BLL.Interfaces;
using TouristGuide.DAL.Repositories;

namespace TouristGuide.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserBL CreateUserService(string connection)
        {
            return new UserBL(new ApplicationDataBase(connection));
        }
    }
}
