using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace wmg.Common.Core
{
    public abstract class ResourceEntity
    {
        protected ResourceEntity()
        {

            CreatedOn = DateTime.Now;

            UpdatedOn = DateTime.Now;
        }

        [Required]
        public string CreatedBy { get; set; }


        public string UpdatedBy { get; set; }


        public DateTime CreatedOn { get; set; }


        public DateTime UpdatedOn { get; set; }
    }
}
