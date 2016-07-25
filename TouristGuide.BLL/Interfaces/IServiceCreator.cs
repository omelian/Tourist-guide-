using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristGuide.BLL.Interfaces
{
    public interface IServiceCreator
    {
        IUserBL CreateUserService(string connection);
    }
}
