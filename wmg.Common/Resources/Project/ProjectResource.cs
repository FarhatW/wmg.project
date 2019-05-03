using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using wmg.Common.Core;
using wmg.Common.Resources.User;

namespace wmg.Common.Resources.Project
{
   public class ProjectResource : ResourceEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProjectTypeResource ProjectTypeId { get; set; }
        public UserResource UserId { get; set; }
        public ProjectDifficultyResource DifficultyID { get; set; }
        public TimeSpan StartedAt { get; set; }
        public TimeSpan EndedAt { get; set; }
        public float M2Number { get; set; }

    }
}
