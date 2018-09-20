using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace wmg.Common.Entites
{
    [Table("Roles")]
    public class Role : IdentityRole<int>
    {

        public Role()
        {
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;

        }
        public bool Enable { get; set; }
        public int Rank { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
