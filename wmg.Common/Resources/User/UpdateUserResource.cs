using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using wmg.Common.Core;

namespace wmg.Common.Resources.User
{
    public class UpdateUserResource: ResourceEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Ncnss { get; set; }
        public string Ncin { get; set; }
        public AddressResource Address { get; set; }
        public string Token { get; set; }

        public ICollection<int> Roles { get; set; }

        public UpdateUserResource()
        {
            Roles = new Collection<int>();
        }
    }
}
