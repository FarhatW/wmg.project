using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;
using wmg.Common.Core;

namespace wmg.Common.Entites
{
    [Table("Users")]
    public class User : IdentityUser<int>
    {
        public User()
        {
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
            UserRoles = new Collection<UserRole>();


        }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }


    }
}
