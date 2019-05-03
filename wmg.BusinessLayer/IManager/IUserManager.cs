using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wmg.BusinessLayer.Core;
using wmg.Common.Resources;
using wmg.Common.Resources.User;

namespace wmg.BusinessLayer.IManager
{
   public interface IUserManager : IActionManager<UserResource>
    {

        Task<UserResource> GetIUserByEmail(string email);
    }
}
