using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoccerManager.Models
{
    public class Team
    {        
        public int TeamId { get; set; }
        [Display(Name = "Team Name")]
        public string TeamName { get; set; }
        [Display(Name = "Cash")]
        public int Money { get; set; }
        public int? CoachId { get; set; }

        [ForeignKey("CoachId")]
        public virtual UserProfile UserProfile { get; set; }
    }
}