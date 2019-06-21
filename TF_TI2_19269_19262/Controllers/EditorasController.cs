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
    public class EditorasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Editoras
        public ActionResult Index()
        {
            return View(db.Editora.ToList());
        }

        // GET: Editoras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Editora editora = db.Editora.Find(id);
            if (editora == null)
            {
                return HttpNotFound();
            }
            return View(editora);
        }

        // GET: Editoras/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Editoras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create([Bind(Include = "ID,Nome,Logo")] Editora editora, HttpPostedFileBase uploadLogo)
        {
            int idNovaEditora = db.Editora.Max(t => t.ID) + 1;

            string nomeFoto = "Editora_" + idNovaEditora + ".jpg";

            string path = "";

            if (uploadLogo != null)
            {
                path = Path.Combine(Server.MapPath("~/Imagens/"), nomeFoto);

                //guardar nome do file na bd
                editora.Logo = nomeFoto;
            }
            else
            {
                ModelState.AddModelError("", "Não foi fornecida uma imagem...");

                return View(editora);
            }
            if (ModelState.IsValid)
            {
                db.Editora.Add(editora);
                db.SaveChanges();
                uploadLogo.SaveAs(path);
                return RedirectToAction("Index");
            }

            return View(editora);
        }

        // GET: Editoras/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Editora editora = db.Editora.Find(id);
            if (editora == null)
            {
                return HttpNotFound();
            }
            return View(editora);
        }

        // POST: Editoras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "ID,Nome,Logo")] Editora editora, HttpPostedFileBase editLogo)
        {
            string novoNome = "";
            string nomeAntigo = "";
            bool haFotoNova = false;
            string caminhoCompleto = "";

            if (ModelState.IsValid)
            {
                try
                {
                    if (editLogo != null)
                    {
                        nomeAntigo = editora.Logo;
                        novoNome = "Editora_" + editora.ID + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(editLogo.FileName).ToLower();
                        editora.Logo = novoNome;
                        haFotoNova = true;

                    }
                    db.Entry(editora).State = EntityState.Modified;
                    db.SaveChanges();

                    if (haFotoNova)
                    {
                        caminhoCompleto = (Path.Combine(Server.MapPath("~/Imagens/"), nomeAntigo));
                        System.IO.File.Delete(caminhoCompleto);
                        editLogo.SaveAs(Path.Combine(Server.MapPath("~/Imagens/"), novoNome));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", string.Format("Ocorreu um erro com a edição da editora {0}", editora.Nome));
                }

            }
            return RedirectToAction("Index");
        }


        // GET: Editoras/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Editora editora = db.Editora.Find(id);
            if (editora == null)
            {
                return HttpNotFound();
            }
            return View(editora);
        }

        // POST: Editoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            Editora editora = db.Editora.Find(id);
            db.Editora.Remove(editora);
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
