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
    public class ComentariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comentarios
        public ActionResult Index()
        {
            var comentarios = db.Comentarios.Include(c => c.Episodio);
            return View(comentarios.ToList());
        }

        // GET: Comentarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Comentarios comentarios = db.Comentarios.Find(id);
            if (comentarios == null)
            {
                return RedirectToAction("Index");
            }
            return View(comentarios);
        }

        // GET: Comentarios/Create
        public ActionResult Create()
        {
            ViewBag.EpisodioFK = new SelectList(db.Episodios, "ID", "Nome");
            return View();
        }

        // POST: Comentarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Os utilizadores do tipo Utilizador e Administrador poderão criar comentários
        [Authorize(Roles = "Utilizador,Administrador")]
        public ActionResult Create([Bind(Include = "ID,Texto,EpisodioFK")] Comentarios comentario, Episodios episodio, string coment)
        {
            var Ut = db.Utilizadores.Where(
                uti => uti.UserName
                .Equals(User.Identity.Name)).FirstOrDefault();

            comentario.UtilizadorFK = Ut.ID;

            comentario.Texto= coment ;

            comentario.EpisodioFK= episodio.ID ;


            if (ModelState.IsValid)
            {
                //guarda os comentarios na BD
                db.Comentarios.Add(comentario);
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewBag.UtilizadorFK = new SelectList(db.Utilizadores, "ID", "UserName", comentario.UtilizadorFK);
            ViewBag.EpisodioFK = new SelectList(db.Episodios, "ID", "Nome", comentario.EpisodioFK);
            return Redirect(Request.UrlReferrer.ToString());
        }

        // GET: Comentarios/Edit/5
        [Authorize(Roles = "Utilizador,Administrador")]
        public ActionResult Edit(int? id)
        {
            
            var Ut = db.Utilizadores.Where(
            uti => uti.UserName.Equals(User.Identity.Name)).FirstOrDefault();

            Comentarios comentario = db.Comentarios.Find(id);

            if (id == null)
            {
                return RedirectToAction("Index");
            }
            try {
                //ve se o coment pertence ao user ou se possui permissoes de admin
                if (Ut.ID.Equals(comentario.UtilizadorFK) || User.IsInRole("Administrador"))
                {

                ViewBag.EpisodioFK = new SelectList(db.Episodios, "ID", "Nome", comentario.EpisodioFK);
                return View(comentario);
                }
            }
            catch(Exception ex)
            {

            }
            return RedirectToAction("Index");
        }

        // POST: Comentarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Utilizador,Administrador")]
        public ActionResult Edit([Bind(Include = "ID,Texto,EpisodioFK")] Comentarios comentario)
        {

            //um user com a role admin pode editar qualquer comentario, um utilizador com a role utilizador apenas pode editar o seu
            var Ut = db.Utilizadores.Where(uti => uti.UserName.Equals(User.Identity.Name)).FirstOrDefault();

            //ve se o coment pertence ao user ou se possui permissoes de admin
            if (Ut.ID.Equals(comentario.UtilizadorFK) || User.IsInRole("Administrador"))
            {
                if (ModelState.IsValid) { 
                comentario.UtilizadorFK = Ut.ID;
                db.Entry(comentario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
                }
            
            ViewBag.EpisodioFK = new SelectList(db.Episodios, "ID", "Nome", comentario.EpisodioFK);
            return View(comentario);
            }
            return RedirectToAction("Index");
        }

        // GET: Comentarios/Delete/5
        [Authorize(Roles = "Administrador,Utilizador")]
        public ActionResult Delete(int? id)
        { Comentarios comentario = db.Comentarios.Find(id);
            //um utilizador com a role Adim pode apagar qualquer comentario
            //um utilizador pode apenas apagar os seus comentarios   

            //pesquisa na base de dados o utilizador que está autenticado
            var Ut = db.Utilizadores.Where(uti => uti.UserName.Equals(User.Identity.Name)).FirstOrDefault();

            try
            {
                if (Ut.ID.Equals(comentario.UtilizadorFK) || User.IsInRole("Administrador"))
                {
                    if (id == null)
                    {
                        return RedirectToAction("Index");
                    }
                    if (comentario == null)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(comentario);
                }
                return RedirectToAction("Details", "Episodios", new { id = comentario.EpisodioFK });
            }
            catch(Exception ex)
            {
                return RedirectToAction("Details", "Episodios", new { id = comentario.EpisodioFK });
            }
        }
        

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Utilizador")]
        public ActionResult DeleteConfirmed(int id)
        {
            Comentarios comentario = db.Comentarios.Find(id);

            var Ut = db.Utilizadores.Where(uti => uti.UserName.Equals(User.Identity.Name)).FirstOrDefault();


            if (Ut.ID.Equals(comentario.UtilizadorFK) || User.IsInRole("Administrador"))
            {
                db.Comentarios.Remove(comentario);
                db.SaveChanges();
                return RedirectToAction("Details", "Episodios", new { id = comentario.EpisodioFK });
            }
            return RedirectToAction("Details", "Episodios", new { id = comentario.EpisodioFK });
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
