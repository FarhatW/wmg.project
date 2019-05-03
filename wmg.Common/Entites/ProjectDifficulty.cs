using System;
using System.Collections.Generic;
using System.Text;
using wmg.Common.Core;

namespace wmg.Common.Entites
{
    public class ProjectDifficulty : BaseEntity
    {
        public int Id { get; set; }
        public string LbDifficulty { get; set; }
        public bool IsEnabled { get; set; }
    }
}
