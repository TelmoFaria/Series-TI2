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
    public class EpisodiosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Episodios
        public ActionResult Index(int?id)
        {
            ViewBag.TemporadaID = id;
            var episodios = db.Episodios.Include(e => e.Temporadas);
            var ep = from p in db.Episodios
                       where p.TemporadaFK == id
                       select p;
            return View(ep.ToList());
        }



        // GET: Episodios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Episodios episodios = db.Episodios.Find(id);
            if (episodios == null)
            {
                return HttpNotFound();
            }
            return View(episodios);
        }

        // GET: Episodios/Create
        public ActionResult Create()
        {
            ViewBag.TemporadaFK = new SelectList(db.Temporadas, "ID", "Nome");
            return View();
        }

        // POST: Episodios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Numero,Nome,Sinopse,Foto,Trailer,Classificacao,TemporadaFK")] Episodios episodios)
        {
            if (ModelState.IsValid)
            {
                db.Episodios.Add(episodios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TemporadaFK = new SelectList(db.Temporadas, "ID", "Nome", episodios.TemporadaFK);
            return View(episodios);
        }

        // GET: Episodios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Episodios episodios = db.Episodios.Find(id);
            if (episodios == null)
            {
                return HttpNotFound();
            }
            ViewBag.TemporadaFK = new SelectList(db.Temporadas, "ID", "Nome", episodios.TemporadaFK);
            return View(episodios);
        }

        // POST: Episodios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Numero,Nome,Sinopse,Foto,Trailer,Classificacao,TemporadaFK")] Episodios episodios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(episodios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TemporadaFK = new SelectList(db.Temporadas, "ID", "Nome", episodios.TemporadaFK);
            return View(episodios);
        }

        // GET: Episodios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Episodios episodios = db.Episodios.Find(id);
            if (episodios == null)
            {
                return HttpNotFound();
            }
            return View(episodios);
        }

        // POST: Episodios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Episodios episodios = db.Episodios.Find(id);
            db.Episodios.Remove(episodios);
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
