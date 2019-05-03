using System;
using System.Collections.Generic;
using System.Text;
using wmg.Common.Core;

namespace wmg.Common.Resources.Project
{
   public class ProjectDifficultyResource : ResourceEntity
    {
        public int Id { get; set; }
        public string LbDifficulty { get; set; }
        public bool IsEnabled { get; set; }

    }
}
