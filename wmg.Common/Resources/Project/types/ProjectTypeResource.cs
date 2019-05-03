using System;
using System.Collections.Generic;
using System.Text;
using wmg.Common.Core;

namespace wmg.Common.Resources.Project
{
   public class ProjectTypeResource : ResourceEntity
    {
        public int Id { get; set; }
        public string LbProject { get; set; }
        public float Credit { get; set; }
        public bool IsEnabled { get; set; }

    }
}
