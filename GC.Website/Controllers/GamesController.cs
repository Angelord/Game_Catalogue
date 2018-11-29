using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameC.Data.Context;
using GameC.Data.Entities;
using PagedList;

namespace GameC.Website.Controllers
{
    public class GamesController : Controller
    {
        private const int PAGE_SIZE = 4;

        private GameCatalogDbContext db = new GameCatalogDbContext();

        // GET: Games
        [AllowAnonymous]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.GenreSortParam = sortOrder == "genre" ? "genre_desc" : "genre";
            ViewBag.RatingSortParam = sortOrder == "rating" ? "rating_desc" : "rating";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IQueryable<Game> games = db.Games.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(g => g.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    games = games.OrderByDescending(g => g.Name);
                    break;
                case "genre":
                    games = games.OrderBy(g => g.GenreId);
                    break;
                case "genre_desc":
                    games = games.OrderByDescending(g => g.GenreId);
                    break;
                case "rating":
                    games = games.OrderBy(g => g.RatingId);
                    break;
                case "rating_desc":
                    games = games.OrderByDescending(g => g.RatingId);
                    break;
                default:
                    games = games.OrderBy(g => g.Name);
                    break;
            }

            int pageNumber = (page ?? 1);
            return View(games.ToPagedList(pageNumber, PAGE_SIZE));
        }

        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "ID", "Name");
            ViewBag.RatingId = new SelectList(db.Ratings, "ID", "Value");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,ReleaseYear,GenreId,RatingId")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(db.Genres, "ID", "Name", game.GenreId);
            ViewBag.RatingId = new SelectList(db.Ratings, "ID", "Value", game.RatingId);
            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreId = new SelectList(db.Genres, "ID", "Name", game.GenreId);
            ViewBag.RatingId = new SelectList(db.Ratings, "ID", "Value", game.RatingId);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,ReleaseYear,GenreId,RatingId")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.Genres, "ID", "Name", game.GenreId);
            ViewBag.RatingId = new SelectList(db.Ratings, "ID", "Value", game.RatingId);
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
