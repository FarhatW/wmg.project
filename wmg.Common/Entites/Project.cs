using System;
using System.ComponentModel.DataAnnotations.Schema;
using wmg.Common.Core;

namespace wmg.Common.Entites
{
   public class Project :  BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("ProjectTypes")]
        public int ProjectTypeId { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [ForeignKey("ProjectDifficulty")]
        public int DifficultyID { get; set; } 
        public TimeSpan StartedAt { get; set; }
        public TimeSpan EndedAt { get; set; }
        public float M2Number { get; set; }
    }
}
