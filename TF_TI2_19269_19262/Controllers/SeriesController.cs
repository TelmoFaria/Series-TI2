using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TF_TI2_19269_19262.Models;

namespace TF_TI2_19269_19262.Controllers
{
    public class SeriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Series
        public ActionResult Index()
        {
            var series = db.Series.Include(s => s.Editora);
            return View(series.ToList());
        }

        // GET: Series/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Series series = db.Series.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }
            return View(series);
        }

        // GET: Series/Create
        public ActionResult Create()
        {
            ViewBag.EditoraFK = new SelectList(db.Editora, "ID", "Nome");
            return View();
        }

        // POST: Series/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,Genero,Sinopse,Video,Classificacao,EditoraFK")] Series series, HttpPostedFileBase uploadFoto)
        {
            int idNovaSerie = db.Series.Max(e => e.ID) + 1;
            series.ID = idNovaSerie;

            string nomeFoto = "Serie_" + idNovaSerie + ".png";

            string path = "";

            if (uploadFoto != null)
            {
                // o ficheiro foi fornecido
                // validar se o q foi fornecido é uma imagem ----> fazer em casa
                // formatar o tamanho da imagem

                // criar o caminho completo até ao sítio onde o ficheiro
                // será guardado
                path = Path.Combine(Server.MapPath("~/Imagens/"), nomeFoto);

                //guardar nome do file na bd
                series.Foto = nomeFoto;
            }
            else
            {
                ModelState.AddModelError("", "Não foi fornecida uma imagem...");
                ViewBag.EditoraFK = new SelectList(db.Editora, "ID", "Nome", series.EditoraFK);

                return View(series);
            }


            if (ModelState.IsValid)
            {
                db.Series.Add(series);
                db.SaveChanges();
                uploadFoto.SaveAs(path);
                return RedirectToAction("Index");
            }

            ViewBag.EditoraFK = new SelectList(db.Editora, "ID", "Nome", series.EditoraFK);
            return View(series);
        }

        // GET: Series/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Series series = db.Series.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }
            ViewBag.EditoraFK = new SelectList(db.Editora, "ID", "Nome", series.EditoraFK);
            return View(series);
        }

        // POST: Series/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Genero,Foto,Sinopse,Video,Classificacao,EditoraFK")] Series series)
        {
            if (ModelState.IsValid)
            {
                db.Entry(series).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EditoraFK = new SelectList(db.Editora, "ID", "Nome", series.EditoraFK);
            return View(series);
        }

        // GET: Series/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Series series = db.Series.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }
            return View(series);
        }

        // POST: Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Series series = db.Series.Find(id);
            db.Series.Remove(series);
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
