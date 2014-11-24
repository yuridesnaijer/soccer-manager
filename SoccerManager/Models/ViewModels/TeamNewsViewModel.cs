using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerManager.Models.ViewModels
{
    public class TeamNewsViewModel
    {
        public List<News> NewsList { get; set; }
        public List<Team> Teamslist { get; set; }
    }
}