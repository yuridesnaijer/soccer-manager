using SoccerManager.Models;
using SoccerManager.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SoccerManager.Controllers
{
    public class HomeController : Controller
    {
        private UsersContext db = new UsersContext();
        public ActionResult Index()
        {
            ViewBag.Message = "Welkom bij de meest gruwelijke soccermanager die ooit is gemaakt (door mij)";

            var viewModel = new TeamNewsViewModel();
            
            viewModel.NewsList = db.News.OrderByDescending(n => n.PostedDate).ToList();          
            viewModel.Teamslist = db.Teams.OrderByDescending(t => t.Money).ToList();

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
