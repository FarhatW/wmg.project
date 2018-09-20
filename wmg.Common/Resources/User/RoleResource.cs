using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using wmg.Common.Core;

namespace wmg.Common.Resources.User
{
    public class RoleResource : ResourceEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool Enable { get; set; }

        [Required]
        public int Rank { get; set; }
    }
}
