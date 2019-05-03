using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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

        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }

        public string Ncnss { get; set; }

        public string Ncin { get; set; }
        //Adress
        [StringLength(255)]
        public string Agency { get; set; }
        [StringLength(255)]
        public string Service { get; set; }
        [StringLength(255)]
        public string Company { get; set; }
        public string StreetNumber { get; set; }
        [StringLength(255)]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [StringLength(255)]
        public string PostalCode { get; set; }
        [StringLength(255)]
        public string City { get; set; }
        public string AddressExtra { get; set; }

    }
}
