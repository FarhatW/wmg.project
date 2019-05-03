using System;
using System.Collections.Generic;
using System.Text;
using wmg.Common.Core;

namespace wmg.Common.Entites
{
   public class ProjectType : BaseEntity
    {
        public int Id { get; set; }
        public string LbProject { get; set; }
        public float Credit { get; set; }
        public bool IsEnabled { get; set; }
    }
}
