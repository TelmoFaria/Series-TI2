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
        /// <summary>
        /// faz get de todos os papeis e pessoas associados a esses papeis
        /// </summary>
        /// <returns>view index com a lista de papeis e pessoas associados aos mesmos</returns>
        public ActionResult Index()
        {
            var pessoasEpisodios = db.PessoasEpisodios.Include(p => p.Episodio).Include(p => p.Pessoa);
            return View(pessoasEpisodios.ToList());
        }

        // GET: PessoasEpisodios/Details/5
        /// <summary>
        /// faz get dos dados de detalhes de 1 papel dado 1 id
        /// </summary>
        /// <param name="id">id do papel</param>
        /// <returns>view details com o papel associado ao id fornecido</returns>
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
        /// <summary>
        /// faz get dos dados dos papeis e devolve viewbags com os episódios (nome e id) e com as pessoas (id e nome) associados a esse papel
        /// </summary>
        /// <returns>view create</returns>
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.EpisodioFK = new SelectList(db.Episodios, "ID", "Nome");
            ViewBag.PessoaFK = new SelectList(db.Pessoas, "ID", "Nome");
            return View();
        }

        // POST: PessoasEpisodios/Create
        /// <summary>
        /// cria um papel na bd 
        /// </summary>
        /// <param name="pessoasEpisodios">papel (papel, pessoaFK e episodioFK)</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create([Bind(Include = "Papel,PessoaFK,EpisodioFK")] PessoasEpisodios pessoasEpisodios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.PessoasEpisodios.Add(pessoasEpisodios);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", string.Format("ocorreu um erro na criação do papel, tente novamente."));
            }


            ViewBag.EpisodioFK = new SelectList(db.Episodios, "ID", "Nome", pessoasEpisodios.EpisodioFK);
            ViewBag.PessoaFK = new SelectList(db.Pessoas, "ID", "Nome", pessoasEpisodios.PessoaFK);
            return View(pessoasEpisodios);
        }

        // GET: PessoasEpisodios/Edit/5
        /// <summary>
        /// faz get dos dados de 1 papel cujo id é o fornecido e devolve viewbags com o episodio (id e nome) e pessoas (id,nome e pessoaFK) 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>view de edit com os dados do papel</returns>
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
        /// <summary>
        /// edita um registo de 1 papel na bd
        /// </summary>
        /// <param name="pessoasEpisodios">papel(id , papel, pessoaFK e EpisodioFK)</param>
        /// <returns>em caso de sucesso retorna para o index, em caso de falha , retorna para a mesma view (edit) com os dados do papel, episodio e pessoas associados</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "ID,Papel,PessoaFK,EpisodioFK")] PessoasEpisodios pessoasEpisodios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(pessoasEpisodios).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", string.Format("ocorreu um erro ao editar o papel, tente novamente."));
            }
            ViewBag.EpisodioFK = new SelectList(db.Episodios, "ID", "Nome", pessoasEpisodios.EpisodioFK);
            ViewBag.PessoaFK = new SelectList(db.Pessoas, "ID", "Nome", pessoasEpisodios.PessoaFK);
            return View(pessoasEpisodios);

        }

        // GET: PessoasEpisodios/Delete/5
        /// <summary>
        /// faz get dos dados ed 1 papel
        /// </summary>
        /// <param name="id">id do papel</param>
        /// <returns>view delete com os dados do papel</returns>
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
        /// <summary>
        /// elimina o registo de 1 papel na bd
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                PessoasEpisodios pessoasEpisodios = db.PessoasEpisodios.Find(id);
                if (pessoasEpisodios == null)
                {
                    return Redirect("/");
                }
                db.PessoasEpisodios.Remove(pessoasEpisodios);
                db.SaveChanges();
            }
            catch
            {
                ModelState.AddModelError("", string.Format("ocorreu um erro ao eliminar o papel, tente novamente."));
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
