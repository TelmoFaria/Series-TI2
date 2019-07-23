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
        /// <summary>
        /// retorna uma lista de editoras vindas da bd
        /// </summary>
        /// <returns>view index com a lista de editoras</returns>
        public ActionResult Index()
        {
            return View(db.Editora.ToList());
        }

        // GET: Editoras/Details/5
        /// <summary>
        /// Devolve a editora cujo id foi o fornecido, e evita erros por a editora ter conteudo nulo ou o id fornecido ser nulo
        /// </summary>
        /// <param name="id">id da editora</param>
        /// <returns> view details com a editora cujo id é o fornecido</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Editora editora = db.Editora.Find(id);
            if (editora == null)
            {
                return RedirectToAction("Index");
            }
            return View(editora);
        }

        // GET: Editoras/Create
        /// <summary>
        /// Devolve a view de create
        /// </summary>
        /// <returns>view create</returns>
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Editoras/Create
        /// <summary>
        /// cria 1 editora na bd e guarda a imagem da editora na bd e apresenta mensagem de erro caso erro aconteça 
        /// </summary>
        /// <param name="editora">editora (nome da editora e Logo)</param>
        /// <param name="uploadLogo">Logo para upload do ficheiro</param>
        /// <returns>retorna a view edit com a editora em caso de erro e em caso de sucesso retorna para a página de index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create([Bind(Include = "Nome,Logo")] Editora editora, HttpPostedFileBase uploadLogo)
        {
            try
            {
                //é fornecido uma editora , cujos atributos sao o id, o nome e o Logo , e ainda é fornecida uma foto

                //é fornecido um novo id para a editora
                int idNovaEditora = db.Editora.Max(t => t.ID) + 1;

                // string com o nome da foto da editora
                string nomeFoto = "Editora_" + idNovaEditora + ".jpg";

                //string que guarda o caminho da foto
                string path = "";

                //caso haja foto
                if (uploadLogo != null)
                {
                    //o caminho é definido
                    path = Path.Combine(Server.MapPath("~/Imagens/"), nomeFoto);

                    // e guardar nome do file na bd
                    editora.Logo = nomeFoto;
                }
                //caso nao haja, é enviada 1 mensagem de erro 
                else
                {
                    ModelState.AddModelError("", "Não foi fornecida uma imagem...");
                    editora.Nome = editora.Nome.Trim();
                    return View(editora);
                }
                //verifica se o modelo é válido
                if (ModelState.IsValid)
                {
                    editora.Nome = editora.Nome.Trim();
                    //se for guarda o registo na bd e guarda a foto na bd
                    db.Editora.Add(editora);
                    db.SaveChanges();
                    uploadLogo.SaveAs(path);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro a criar a editora,tente novamente."));
            }
            

            return View(editora);
        }

        // GET: Editoras/Edit/5
        /// <summary>
        /// devolve a editora associada ao id fornecido
        /// </summary>
        /// <param name="id">id da editora</param>
        /// <returns>view Edit com os dados da editora</returns>
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            //se o id for nulo , faz redirect para a página Home
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            //caso não seja,procura a editora cujo o id é o fornecido
            Editora editora = db.Editora.Find(id);
            if (editora == null)
            {
                return RedirectToAction("Index");
            }
            //devolve a editora 
            return View(editora);
        }

        // POST: Editoras/Edit/5
        /// <summary>
        /// edita 1 registo de editora na bd guardando tb um ficheiro (logo). Em caso de erro devolve uma mensagem de erro
        /// </summary>
        /// <param name="editora">informações da editora (id,nome e logo)</param>
        /// <param name="editLogo">ficheiro Logo</param>
        /// <returns>redirect para a pagina Editoras/Index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "ID,Nome,Logo")] Editora editora, HttpPostedFileBase editLogo)
        {
            //conjunto de variaveis auxiliares para guardar o caminho e o nome da imagem
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
                        //caso o utilizador tenha fornecido uma imagem, e guardado o novo nome da imagem 
                        nomeAntigo = editora.Logo;
                        novoNome = "Editora_" + editora.ID + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(editLogo.FileName).ToLower();
                        editora.Logo = novoNome;
                        //variável para verificar a existencia de foto
                        haFotoNova = true;

                    }
                    //são guardadas as alterações
                    editora.Nome = editora.Nome.Trim();
                    db.Entry(editora).State = EntityState.Modified;
                    db.SaveChanges();

                    //verifica se existe foto
                    if (haFotoNova)
                    {
                        //se houver, guarda a imagem
                        caminhoCompleto = (Path.Combine(Server.MapPath("~/Imagens/"), nomeAntigo));
                        System.IO.File.Delete(caminhoCompleto);
                        editLogo.SaveAs(Path.Combine(Server.MapPath("~/Imagens/"), novoNome));
                    }
                }
                //caso contrário , envia 1 mensagem de erro
                catch (Exception)
                {
                    ModelState.AddModelError("", string.Format("Ocorreu um erro com a edição da editora,tente novamente.", editora.Nome));
                }

            }
            return RedirectToAction("Index");
        }


        // GET: Editoras/Delete/5
        /// <summary>
        /// devolve a editora cujo id é o fornecido
        /// </summary>
        /// <param name="id">id da editora</param>
        /// <returns>view delete com a editora</returns>
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Editora editora = db.Editora.Find(id);
            if (editora == null)
            {
                return RedirectToAction("Index");
            }
            return View(editora);
        }

        // POST: Editoras/Delete/5
        /// <summary>
        /// confirma o eliminar da editora, eliminando o registo da db e returnando para a página Index.devolve mensagem de erro em caso de erro
        /// </summary>
        /// <param name="id">id da editora</param>
        /// <returns>view delete com a editora ou em caso de sucesso retorna para o index</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            //procura pelo editora cujo id é o fornecido
            Editora editora = db.Editora.Find(id);
            if (editora == null)
            {
                return Redirect("/");
            }
            try
            {
                //caso encontre , elimina da bd o registo e o utilizador é redirecionado para a página Ediroras Index
                db.Editora.Remove(editora);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //caso contrario é enviada 1 mensagem de erro
            catch (Exception)
            {
                ModelState.AddModelError("", string.Format("Não é possível apagar esta editora pois existem Séries a ela associados"));
            }
            return View(editora);
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
