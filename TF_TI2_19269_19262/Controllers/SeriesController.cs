using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
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
        //------------------------------------------------------------------------------------
        //                             Tentar ir para a pagina Temporadas 
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
        //------------------------------------------------------------------------------------
        // GET: Series/Create
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
        public ActionResult Create([Bind(Include = "ID,Nome,Genero,Sinopse,Video,Classificacao,EditoraFK")] Series serie, HttpPostedFileBase uploadFoto)
        {
            int idNovaSerie = db.Series.Max(s => s.ID) + 1;
            serie.ID = idNovaSerie;

            string nomeFoto = "Serie_" + idNovaSerie + ".jpg";

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
                serie.Foto = nomeFoto;
            }
            else
            {
                ModelState.AddModelError("", "Não foi fornecida uma imagem...");
                ViewBag.EditoraFK = new SelectList(db.Editora, "ID", "Nome", serie.EditoraFK);

                return View(serie);
            }


            if (ModelState.IsValid)
            {
                db.Series.Add(serie);
                db.SaveChanges();
                uploadFoto.SaveAs(path);
                return RedirectToAction("Index");
            }

            ViewBag.EditoraFK = new SelectList(db.Editora, "ID", "Nome", serie.EditoraFK);
            return View(serie);
        }

        // GET: Series/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Series serie = db.Series.Find(id);
            if (serie == null)
            {
                return HttpNotFound();
            }
            ViewBag.EditoraFK = new SelectList(db.Editora, "ID", "Nome", serie.EditoraFK);
            return View(serie);
        }

        // POST: Series/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "ID,Nome,Genero,Foto,Sinopse,Video,Classificacao,EditoraFK")] Series serie, HttpPostedFileBase editFoto)
        {
            string novoNome = "";
            string nomeAntigo = "";
            bool haFotoNova = false;

            if (ModelState.IsValid)
            {
                 try
                 {
                if (editFoto != null)
                {
                    nomeAntigo = serie.Foto;
                    novoNome = "Serie_" + serie.ID + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(editFoto.FileName).ToLower();
                    serie.Foto = novoNome;
                    haFotoNova = true;
                }
                db.Entry(serie).State = EntityState.Modified;
                db.SaveChanges();
                if (haFotoNova)
                {
                    System.IO.File.Delete(Path.Combine(Server.MapPath("~/Imagens"), nomeAntigo));
                    editFoto.SaveAs(Path.Combine(Server.MapPath("~/Imagens"), novoNome));
                }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", string.Format("Ocorreu um erro com a edição dos dados da serie {0}", serie.Nome));
                }
            }
            ViewBag.EditoraFK = new SelectList(db.Editora, "ID", "Nome", serie.EditoraFK);
            return RedirectToAction("Index");

        }

        // GET: Series/Delete/5
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            Series serie = db.Series.Find(id);
            try { 
            db.Series.Remove(serie);
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", string.Format("Não é possível apagar a série pois existem temporadas a ela associados"));
            }
            return View(serie);
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
