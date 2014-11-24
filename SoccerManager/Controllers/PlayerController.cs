using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoccerManager.Models;
using SoccerManager.Filters;
using WebMatrix.WebData;

namespace SoccerManager.Controllers
{
    [InitializeSimpleMembership]
    [Authorize(Roles = "admin, user")]
    public class PlayerController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Player/
        [AllowAnonymous]
        public ActionResult Index()
        {
            var players = db.Players.Include(p => p.Team);
            return View(players.ToList());
        }


        public ActionResult ForSale()
        {
            List<Player> players = db.Players.Where(p => p.TeamId == null).ToList();
            return View(players);
        }

        public ActionResult Buy(int id = 0)
        {
            //pak de speler die gekocht moet worden
            Player player = db.Players.Find(id);

            // sla de prijs van de speler op
            var price = db.Entry(player).Entity.price;
            // sla de id van de user die nu is ingelogd op
            var userId = WebSecurity.CurrentUserId;
            //sla het team op dat bij de huidige user hoort
            var team = db.Teams.Find(userId);
            //sla de money van dat team op
            var teamMoney = team.Money;
            //kijken of er genoeg geld is
            if (price <= teamMoney)
            {
                db.Entry(team).State = EntityState.Modified;
                //price van het teamgeld afhalen
                team.Money -= price;

                db.Entry(player).State = EntityState.Modified;
                //team id toevoegen aan de speler
                player.TeamId = team.TeamId;
                db.SaveChanges();
                ViewBag.Buy = true;
            }
            else
            {
                ViewBag.buy = false;
            }

            return View();
        }

        public ActionResult Sell(int id = 0)
        {
            Player player = db.Players.Find(id);
            var price = player.price;
            var userId = WebSecurity.CurrentUserId;
            var team = db.Teams.Find(userId);
            var teamMoney = team.Money;

            db.Entry(team).State = EntityState.Modified;
            team.Money += price;

            db.Entry(player).State = EntityState.Modified;
            player.TeamId = null;
            db.SaveChanges();

            return View("ShowPlayers");
        }

        //
        // GET: /Player/Details/5
        public ActionResult Details(int id = 0)
        {
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        //
        // GET: /Player/Create

        public ActionResult Create()
        {
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "TeamName");
            return View();
        }

        //
        // POST: /Player/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Player player)
        {
            if (ModelState.IsValid)
            {
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "TeamName", player.TeamId);
            return View(player);
        }

        //
        // GET: /Player/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "TeamName", player.TeamId);
            return View(player);
        }

        //
        // POST: /Player/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "TeamName", player.TeamId);
            return View(player);
        }

        //
        // GET: /Player/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        //
        // POST: /Player/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}