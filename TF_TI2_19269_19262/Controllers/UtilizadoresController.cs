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
    public class UtilizadoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Utilizadores
        /// <summary>
        /// faz get de todos os utilizadores
        /// </summary>
        /// <returns>view index com 1 lista com todos os utilizadores</returns>
        public ActionResult Index()
        {
            return View(db.Utilizadores.ToList());
        }

        // GET: Utilizadores/Details/5
        /// <summary>
        /// faz get do utilizador cujo id é o fornecido
        /// </summary>
        /// <param name="id">id do utilizador</param>
        /// <returns>view details com os dados do utilizador</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Utilizadores utilizadores = db.Utilizadores.Find(id);
            if (utilizadores == null)
            {
                return RedirectToAction("Index");
            }
            return View(utilizadores);
        }

        // GET: Utilizadores/Create
        /// <summary>
        /// devolve a view create
        /// </summary>
        /// <returns>view create</returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: Utilizadores/Create
        /// <summary>
        /// cria 1 registo de 1 utilizador na bd
        /// </summary>
        /// <param name="utilizadores">utilizador(ED,Email,Nome e UserName)</param>
        /// <returns>view create com os dados do utilizador ou em caso de sucesso , retorna para o index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,Nome,UserName")] Utilizadores utilizadores)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Utilizadores.Add(utilizadores);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", string.Format("Não é possível criar este utilizador"));
            }
            

            return View(utilizadores);
        }

        // GET: Utilizadores/Edit/5
        /// <summary>
        /// faz get dos dados do utilizador cujo id é o fornecido
        /// </summary>
        /// <param name="id">id do utilizador</param>
        /// <returns>view edit com os dados do utilizador</returns>
        [Authorize(Roles = "Restrito")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Utilizadores utilizadores = db.Utilizadores.Find(id);
            if (utilizadores == null)
            {
                return RedirectToAction("Index");
            }
            return View(utilizadores);
        }

        // POST: Utilizadores/Edit/5
        /// <summary>
        /// edita um registo de 1 utilizador na bd
        /// </summary>
        /// <param name="utilizadores">utilizador (ID,Email,Nome,UserName)</param>
        /// <returns>em caso de sucesso retorna para o index e em caso de erro retorna para a mesma view com os dados do utilizador</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Restrito")]
        public ActionResult Edit([Bind(Include = "ID,Email,Nome,UserName")] Utilizadores utilizadores)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(utilizadores).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception)
            {
                ModelState.AddModelError("", string.Format("Não foi possivel editar este utilizador"));
            }
            
            return View(utilizadores);
        }

        // GET: Utilizadores/Delete/5
        /// <summary>
        /// faz get do utilizador cujo id é o fornecido
        /// </summary>
        /// <param name="id">id do utilizador</param>
        /// <returns>view delete com o utilizador</returns>
        [Authorize(Roles = "Restrito")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Utilizadores utilizadores = db.Utilizadores.Find(id);
            if (utilizadores == null)
            {
                return RedirectToAction("Index");
            }
            return View(utilizadores);
        }

        // POST: Utilizadores/Delete/5
        /// <summary>
        /// elimina o registo do utilizador cujo id é o fornecido
        /// </summary>
        /// <param name="id"></param>
        /// <returns>retorna para o index</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Restrito")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Utilizadores utilizadores = db.Utilizadores.Find(id);
                db.Utilizadores.Remove(utilizadores);
                db.SaveChanges();
            }
            catch (Exception)
            {
                ModelState.AddModelError("", string.Format("Não foi possivel apagar este utilizador"));
            }
            
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
