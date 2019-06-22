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
    public class TemporadasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Temporadas
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return Redirect("/");
            }
            var temporada = db.Temporadas.Include(t => t.Series);
            var temp = from t in db.Temporadas
                       where t.SerieFK == id
                         select t;
            return View(temp.ToList());
        }
        // GET: Temporadas/select{id}
        public ActionResult Temp(int id)
        {
            
            var result = from r in db.Temporadas
                         where r.SerieFK == id
                         select r;
            return View(result);
        }

        // GET: Temporadas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporadas temporada = db.Temporadas.Find(id);
            if (temporada == null)
            {
                return HttpNotFound();
            }
            return View(temporada);
        }


        // GET: Temporadas/Create
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
        public ActionResult Create([Bind(Include = "ID,Numero,Nome,Trailer,SerieFK")] Temporadas temporada, HttpPostedFileBase uploadFoto)
        {
            int idNovaTemporada = db.Temporadas.Max(t => t.ID) + 1 ;

            string nomeFoto = "Temporada_" + idNovaTemporada + ".jpg";

            string path = "";

            if (uploadFoto != null)
            {
                path = Path.Combine(Server.MapPath("~/Imagens/"), nomeFoto);

                //guardar nome do file na bd
                temporada.Foto = nomeFoto;
            }
            else
            {
                ModelState.AddModelError("", "Não foi fornecida uma imagem...");
                ViewBag.SerieFK = new SelectList(db.Series, "ID", "Nome", temporada.SerieFK);

                return View(temporada);
            }
            if (ModelState.IsValid)
            {
                db.Temporadas.Add(temporada);
                db.SaveChanges();
                uploadFoto.SaveAs(path);
                return RedirectToAction("Index");
            }

            ViewBag.SerieFK = new SelectList(db.Series, "ID", "Nome", temporada.SerieFK);
            return View(temporada);
        }

        // GET: Temporadas/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporadas temporada = db.Temporadas.Find(id);
            if (temporada == null)
            {
                return HttpNotFound();
            }
            ViewBag.SerieFK = new SelectList(db.Series, "ID", "Nome", temporada.SerieFK);
            return View(temporada);
        }

        // POST: Temporadas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "ID,Numero,Nome,Foto,Trailer,SerieFK")] Temporadas temporada, HttpPostedFileBase editFoto)
        {
            string novoNome = "";
            string nomeAntigo = "";
            bool haFotoNova = false;
            string caminhoCompleto = "";

            if (ModelState.IsValid)
            {
                try
                {
                    if (editFoto != null)
                    {
                        nomeAntigo = temporada.Foto;
                        novoNome = "Temporada_" + temporada.ID + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(editFoto.FileName).ToLower();
                        temporada.Foto = novoNome;
                        haFotoNova = true;

                    }
                    db.Entry(temporada).State = EntityState.Modified;
                    db.SaveChanges();

                    if (haFotoNova) {
                        caminhoCompleto = (Path.Combine(Server.MapPath("~/Imagens/"), nomeAntigo));
                        System.IO.File.Delete(caminhoCompleto);
                        editFoto.SaveAs(Path.Combine(Server.MapPath("~/Imagens/"), novoNome));
                    }
                } catch (Exception ex)
                {
                    ModelState.AddModelError("", string.Format("Ocorreu um erro com a edição da temporada {0}", temporada.Nome));
                }

            }
            ViewBag.SerieFK = new SelectList(db.Series, "ID", "Nome", temporada.SerieFK);
            return RedirectToAction("Index");
        }

        // GET: Temporadas/Delete/5
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            Temporadas temporada = db.Temporadas.Find(id);
            try
            {
                db.Temporadas.Remove(temporada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", string.Format("Não é possível apagar esta temporada pois existem episódios a ela associados"));
            }
            return View(temporada);
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
