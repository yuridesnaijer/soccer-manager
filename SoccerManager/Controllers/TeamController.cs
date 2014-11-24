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
    [Authorize(Roles = "admin")]
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

            ViewBag.PlayersList = pl;

            return View(team);
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