using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoccerManager.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        [Display(Name = "Player Name")]
        public string PlayerName { get; set; }
        public int price { get; set; }
        public int? TeamId { get; set; }

        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }
    }
}