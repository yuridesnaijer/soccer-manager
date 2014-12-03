using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoccerManager.Models;
using SoccerManager.Filters;

namespace SoccerManager.Controllers
{
    [InitializeSimpleMembership]
    [Authorize(Roles = "admin, coach")]
    public class TeamController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Team/
        [AllowAnonymous]
        public ActionResult Index()
        {
            var teams = db.Teams.Include(t => t.UserProfile);
            return View(teams.ToList());
        }

        //
        // GET: /Team/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id = 0)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }

            List<Player> pl = db.Players.Where(p => p.TeamId == id).ToList();

            //totale waarde van het team berekenen
            var value = team.Money;
            foreach (var player in pl)
            {
                value += player.price;
            }

            ViewBag.PlayersList = pl;
            ViewBag.Value = value;

            return View(team);
        }

        //match/teamid
        [AllowAnonymous]
        public ActionResult Match(int id = 0)
        {
            Team userTeam = db.Teams.Find(id);

            

            //check of team 11 spelers heeft
            if(db.Players.Where(p => p.TeamId == id).Count() != 11){
                ViewBag.Result = "You don't have 11 players in your team! buy or sell some!";
                return View("Match");
            }

            var otherTeams = db.Teams.Where(t => t.TeamId != id).ToArray();
            var r = new Random();

            if(r.Next(2000) < 1000){
                //win
                //een random prijs genereren en optellen
                var prize = r.Next(50000, 1200000);
                
                db.Entry(userTeam);
                userTeam.Money += prize;
                db.SaveChanges();

                ViewBag.Prize = prize;
                ViewBag.Result = "You won! " + prize + " prize money is received!";
            } else{
                //lose
                var prize = r.Next(50000, 700000);

                db.Entry(userTeam);
                userTeam.Money -= prize;
                db.SaveChanges();

                ViewBag.Result = "You lost! " + prize + " prize money is taken.";
            }
            //hier wordt de tegenstander uitgezocht.
            ViewBag.Opponent = (Team)otherTeams[r.Next(otherTeams.Count())];

            return View("Match");
        }

        //
        // GET: /Team/Create

        public ActionResult Create()
        {
            ViewBag.CoachId = new SelectList(db.UserProfiles, "UserId", "UserName");
            return View();
        }

        //
        // POST: /Team/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Team team)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CoachId = new SelectList(db.UserProfiles, "UserId", "UserName", team.CoachId);
            return View(team);
        }

        //
        // GET: /Team/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            ViewBag.CoachId = new SelectList(db.UserProfiles, "UserId", "UserName", team.CoachId);
            return View(team);
        }

        //
        // POST: /Team/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CoachId = new SelectList(db.UserProfiles, "UserId", "UserName", team.CoachId);
            return View(team);
        }

        //
        // GET: /Team/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        //
        // POST: /Team/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
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