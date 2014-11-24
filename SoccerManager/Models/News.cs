using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoccerManager.Models
{
    public class News
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        [Display(Name = "Posted on")]
        public DateTime? PostedDate { get; set; }
    }
}