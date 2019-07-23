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
        /// <summary>
        /// recebe da bd os dados dos comentários e dos episódios
        /// </summary>
        /// <returns> retorna a view index com a lista de comentários com os episódios associados</returns>
        public ActionResult Index()
        {
            var comentarios = db.Comentarios.Include(c => c.Episodio);
            return View(comentarios.ToList());
        }

        // GET: Comentarios/Details/5
        /// <summary>
        /// devolve os comentarários associados ao id, e faz redirect para o Index em caso de erro 
        /// </summary>
        /// <param name="id"> recebe o id do comentário</param>
        /// <returns> devolvea view details com o comentário associado ao id</returns>
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
        /// <summary>
        /// cria 1 viewbag com o id e nome do episódio associado ao comentário
        /// </summary>
        /// <param name="id"> recebe o id do comentário</param>
        /// <returns>view create</returns>
        public ActionResult Create(int? id)
        {
            ViewBag.EpisodioFK = new SelectList(db.Episodios, "ID", "Nome");
            return View();
        }

        // POST: Comentarios/Create
        /// <summary>
        /// cria um comentario na bd, em caso de erro mostra 1 messagem de erro
        /// </summary>
        /// <param name="comentario"> recebe um comentário com o texto e o episodioFK</param>
        /// <returns> viewbags com utilizadorFK e episodioFK</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Os utilizadores do tipo Utilizador e Administrador poderão criar comentários
        [Authorize(Roles = "Utilizador,Administrador")]
        public ActionResult Create([Bind(Include = "Texto,EpisodioFK")] Comentarios comentario)
        {
            try{
                var Ut = db.Utilizadores.Where(
                uti => uti.UserName
                .Equals(User.Identity.Name)).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(comentario.Texto))
            {
                comentario.UtilizadorFK = Ut.ID;
                
                if (ModelState.IsValid)
            {
                //guarda os comentários na BD
                db.Comentarios.Add(comentario);
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewBag.UtilizadorFK = new SelectList(db.Utilizadores, "ID", "UserName", comentario.UtilizadorFK);
            ViewBag.EpisodioFK = new SelectList(db.Episodios, "ID", "Nome", comentario.EpisodioFK);
            }
            }
            catch(Exception)
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro a Criar o comentário"));
            }
            
            
            return Redirect(Request.UrlReferrer.ToString());
        }

        // GET: Comentarios/Edit/5
        /// <summary>
        /// verifica qual é o utilizador e se tem permissões para editar 
        /// </summary>
        /// <param name="id"> recebe o id do cometário</param>
        /// <returns>redirect para a view com os comentarios e viewbag com episodioFK</returns>
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
                ModelState.AddModelError("", string.Format("Ocorreu um erro a Editar o comentário"));
            }
            return RedirectToAction("Index");
        }

        // POST: Comentarios/Edit/5
        /// <summary>
        /// edita o registo de comentário na bd
        /// </summary>
        /// <param name="comentario"> comentario (id, texto e episodioFK)</param>
        /// <returns>mensagem de erro caso ocorra e viewbag com episodioFK e return para a view index caso sucesso</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Utilizador,Administrador")]
        public ActionResult Edit([Bind(Include = "ID,Texto,EpisodioFK")] Comentarios comentario)
        {
            try{
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
            }
            catch
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro a Editar o comentário"));
            }
            
            return RedirectToAction("Index");
        }

        // GET: Comentarios/Delete/5
        /// <summary>
        /// verifica o utilizador que esta a tentar eliminar 
        /// </summary>
        /// <param name="id"> id do comentário</param>
        /// <returns>retorna o comentário associado a 1 id, ou mensagem de erro caso ocorra, e caso sucesso volta para a página de detalhes do episódio</returns>
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
                
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro "));
            }
            return RedirectToAction("Details", "Episodios", new { id = comentario.EpisodioFK });
        }
        

        // POST: Comentarios/Delete/5
        /// <summary>
        /// confirma o eliminar do comentário , verificando o utilizador que o fez e se tem autorização,em caso de sucesso guarda as alterações na bd e se nao devolve uma mensagem de erro 
        /// </summary>
        /// <param name="id"> id do comentário</param>
        /// <returns>volta para a página de detalhes do episódio</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Utilizador")]
        public ActionResult DeleteConfirmed(int id)
        {
            Comentarios comentario = db.Comentarios.Find(id);
            try
            {
            var Ut = db.Utilizadores.Where(uti => uti.UserName.Equals(User.Identity.Name)).FirstOrDefault();

                if (Ut.ID.Equals(comentario.UtilizadorFK) || User.IsInRole("Administrador"))
                {
                    db.Comentarios.Remove(comentario);
                    db.SaveChanges();
                }
            }
            catch
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro a Eliminar o comentário"));
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
