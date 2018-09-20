using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace wmg.Common.Entites
{
    public class UserRole : IdentityUserRole<int>
    {
        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}
