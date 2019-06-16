using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TF_TI2_19269_19262.Models;

namespace TF_TI2_19269_19262.Controllers
{
    public class TemporadasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Temporadas
        public ActionResult Index()
        {
            var temporadas = db.Temporadas.Include(t => t.Series);
            return View(temporadas.ToList());
        }

        // GET: Temporadas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporadas temporadas = db.Temporadas.Find(id);
            if (temporadas == null)
            {
                return HttpNotFound();
            }
            return View(temporadas);
        }
        // GET: Series/Temporadas/5
        public ActionResult Temporadas(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var temporadas = db.Temporadas.Where(t => t.SerieFK == id);
            var varSerie = db.Series;
            if (temporadas == null)
            {
                return HttpNotFound();
            }
            return View(temporadas);
        }

        // GET: Temporadas/Create
        public ActionResult Create()
        {
            ViewBag.SerieFK = new SelectList(db.Series, "ID", "Nome");
            return View();
        }

        // POST: Temporadas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Numero,Nome,Foto,Trailer,SerieFK")] Temporadas temporadas)
        {
            if (ModelState.IsValid)
            {
                db.Temporadas.Add(temporadas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SerieFK = new SelectList(db.Series, "ID", "Nome", temporadas.SerieFK);
            return View(temporadas);
        }

        // GET: Temporadas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporadas temporadas = db.Temporadas.Find(id);
            if (temporadas == null)
            {
                return HttpNotFound();
            }
            ViewBag.SerieFK = new SelectList(db.Series, "ID", "Nome", temporadas.SerieFK);
            return View(temporadas);
        }

        // POST: Temporadas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Numero,Nome,Foto,Trailer,SerieFK")] Temporadas temporadas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(temporadas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SerieFK = new SelectList(db.Series, "ID", "Nome", temporadas.SerieFK);
            return View(temporadas);
        }

        // GET: Temporadas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporadas temporadas = db.Temporadas.Find(id);
            if (temporadas == null)
            {
                return HttpNotFound();
            }
            return View(temporadas);
        }

        // POST: Temporadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Temporadas temporadas = db.Temporadas.Find(id);
            db.Temporadas.Remove(temporadas);
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
