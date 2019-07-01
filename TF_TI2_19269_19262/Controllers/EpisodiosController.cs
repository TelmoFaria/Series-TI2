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
    public class EpisodiosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Episodios
        public ActionResult Index(int?id)
        {
            if (id == null)
            {
                return Redirect("/");
            }
            //ViewBag.SerieID = db.Temporadas.Find(id).SerieFK;
            //var episodios = db.Episodios.Include(e => e.Temporadas);
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
                return RedirectToAction("Index");
            }
            Episodios episodio = db.Episodios.Find(id);
            if (episodio == null)
            {
                return HttpNotFound();
            }
            var coment = episodio.ListaDeComentarios.ToList();
            ViewBag.coment = coment;

            return View(episodio);
        }

        // GET: Episodios/Create
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
        public ActionResult Create([Bind(Include = "ID,Numero,Nome,Sinopse,Foto,Trailer,AuxClassificacao,TemporadaFK")] Episodios episodio, HttpPostedFileBase uploadFoto)
        {
            episodio.Classificacao = Convert.ToDouble(episodio.AuxClassificacao);

            int idNovoEpisodio = db.Episodios.Max(t => t.ID) + 1;

            string nomeFoto = "Episodio_" + idNovoEpisodio + ".jpg";

            string path = "";

            if (uploadFoto != null)
            {
                path = Path.Combine(Server.MapPath("~/Imagens/"), nomeFoto);

                //guardar nome do file na bd
                episodio.Foto = nomeFoto;
            }
            else
            {
                ModelState.AddModelError("", "Não foi fornecida uma imagem.");
                ViewBag.TemporadaFK = new SelectList(db.Temporadas, "ID", "Nome", episodio.TemporadaFK);

                return View(episodio);
            }
            if (ModelState.IsValid)
            {
                db.Episodios.Add(episodio);
                db.SaveChanges();
                uploadFoto.SaveAs(path);
                //tenho de passar para aqui o id que uso
                return RedirectToAction("Index", new { id = episodio.TemporadaFK});
            }

            ViewBag.TemporadaFK = new SelectList(db.Temporadas, "ID", "Nome", episodio.TemporadaFK);
            return View(episodio);
        }

        // GET: Episodios/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
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
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "ID,Numero,Nome,Sinopse,Foto,Trailer,AuxClassificacao,TemporadaFK")] Episodios episodio, HttpPostedFileBase editFoto)
        {
            episodio.Classificacao = Convert.ToDouble(episodio.AuxClassificacao);
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
                        nomeAntigo = episodio.Foto;
                        novoNome = "Episodio_" + episodio.ID + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(editFoto.FileName).ToLower();
                        episodio.Foto = novoNome;
                        haFotoNova = true;

                    }
                    db.Entry(episodio).State = EntityState.Modified;
                    db.SaveChanges();

                    if (haFotoNova)
                    {
                        caminhoCompleto = (Path.Combine(Server.MapPath("~/Imagens/"), nomeAntigo));
                        System.IO.File.Delete(caminhoCompleto);
                        editFoto.SaveAs(Path.Combine(Server.MapPath("~/Imagens/"), novoNome));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", string.Format("Ocorreu um erro com a edição do episodio {0}", episodio.Nome));
                }

            }
            ViewBag.TemporadaFK = new SelectList(db.Temporadas, "ID", "Nome", episodio.TemporadaFK);
            return RedirectToAction("Index");
        }

        // GET: Episodios/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
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
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            Episodios episodio = db.Episodios.Find(id);
            try
            {
                db.Episodios.Remove(episodio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", string.Format("Não é possível apagar este episodio pois existem comentários ou pessoas a ele associados"));
            }
            return View(episodio);
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
