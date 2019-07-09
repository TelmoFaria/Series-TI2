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
    //A Classe pessoasEpisodios representa os papeis de uma pessoa dado o episodio em que participam
    public class PessoasEpisodiosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PessoasEpisodios
        public ActionResult Index()
        {
            var pessoasEpisodios = db.PessoasEpisodios.Include(p => p.Episodio).Include(p => p.Pessoa);
            return View(pessoasEpisodios.ToList());
        }

        // GET: PessoasEpisodios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //alterar as rotas por defeito, de modo a não haver erros de BadRequest ou de NotFound 
                return RedirectToAction("Index");
            }
            PessoasEpisodios pessoasEpisodios = db.PessoasEpisodios.Find(id);
            if (pessoasEpisodios == null)
            {
                return RedirectToAction("Index");
            }
            return View(pessoasEpisodios);
        }

        // GET: PessoasEpisodios/Create
        //Apenas os utilizadores do tipo "Administrador podem criar, editar ou eliminar Papeis
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.EpisodioFK = new SelectList(db.Episodios, "ID", "Nome");
            ViewBag.PessoaFK = new SelectList(db.Pessoas, "ID", "Nome");
            return View();
        }

        // POST: PessoasEpisodios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create([Bind(Include = "ID,Papel,PessoaFK,EpisodioFK")] PessoasEpisodios pessoasEpisodios)
        {
            if (ModelState.IsValid)
            {
                db.PessoasEpisodios.Add(pessoasEpisodios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EpisodioFK = new SelectList(db.Episodios, "ID", "Nome", pessoasEpisodios.EpisodioFK);
            ViewBag.PessoaFK = new SelectList(db.Pessoas, "ID", "Nome", pessoasEpisodios.PessoaFK);
            return View(pessoasEpisodios);
        }

        // GET: PessoasEpisodios/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            PessoasEpisodios pessoasEpisodios = db.PessoasEpisodios.Find(id);
            if (pessoasEpisodios == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.EpisodioFK = new SelectList(db.Episodios, "ID", "Nome", pessoasEpisodios.EpisodioFK);
            ViewBag.PessoaFK = new SelectList(db.Pessoas, "ID", "Nome", pessoasEpisodios.PessoaFK);
            return View(pessoasEpisodios);
        }

        // POST: PessoasEpisodios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "ID,Papel,PessoaFK,EpisodioFK")] PessoasEpisodios pessoasEpisodios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pessoasEpisodios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EpisodioFK = new SelectList(db.Episodios, "ID", "Nome", pessoasEpisodios.EpisodioFK);
            ViewBag.PessoaFK = new SelectList(db.Pessoas, "ID", "Nome", pessoasEpisodios.PessoaFK);
            return View(pessoasEpisodios);
        }

        // GET: PessoasEpisodios/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            PessoasEpisodios pessoasEpisodios = db.PessoasEpisodios.Find(id);
            if (pessoasEpisodios == null)
            {
                return RedirectToAction("Index");
            }
            return View(pessoasEpisodios);
        }

        // POST: PessoasEpisodios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            PessoasEpisodios pessoasEpisodios = db.PessoasEpisodios.Find(id);
            db.PessoasEpisodios.Remove(pessoasEpisodios);
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
