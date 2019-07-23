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
        //----Pedidos de Get à bd 
        // GET: Temporadas
        /// <summary>
        /// faz get dos dados de  todas as temporadas de 1 determinada série 
        /// </summary>
        /// <param name="id">id da série</param>
        /// <returns>view index com as temporadas de 1 série</returns>
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return Redirect("/");
            }
            var temp = from t in db.Temporadas
                       where t.SerieFK == id
                         select t;

            ViewBag.SerieFK = id;
            return View(temp.ToList());
        }

        // GET: Temporadas/Details/5
        /// <summary>
        /// get de todos os dados de 1 temporada cujo id é o fornecido
        /// </summary>
        /// <param name="id">id da temporada</param>
        /// <returns>view details com os dados da temporada</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return Redirect("/");
            }
            Temporadas temporada = db.Temporadas.Find(id);
            if (temporada == null)
            {
                return Redirect("/");
            }
            return View(temporada);
        }


        /// <summary>
        /// devolve o SerieFK num viewbag
        /// </summary>
        /// <param name="id">id da série</param>
        /// <returns>view create</returns>
        //Get
        [Authorize(Roles = "Administrador")]
        public ActionResult Create(int id)
        {
            ViewBag.SerieFK = id;
            return View();
        }
        //---
        // POST: Temporadas/Create
        /// <summary>
        /// cira 1 registo de temporada na bd incluido a imagem da mesma. devolve mensagem de erro em caso de erro 
        /// </summary>
        /// <param name="temporada">temporada(Numero , Nome, Trailer e SerieFK)</param>
        /// <param name="uploadFoto">ficheiro de imagem</param>
        /// <returns>em caso de sucesso retorna para o id</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create([Bind(Include = "Numero,Nome,Trailer,SerieFK")] Temporadas temporada, HttpPostedFileBase uploadFoto)
        {
            try
            {
                // definir o novo id para a temporada
                int idNovaTemporada = db.Temporadas.Max(t => t.ID) + 1 ;

            // definir o nome da foto
            string nomeFoto = "Temporada_" + idNovaTemporada + ".jpg";

            //caminho da foto
            string path = "";

            //verifica se o utilizador forneceu imagem
            if (uploadFoto != null)
            {
                //caso tenha fornecido , guarda o caminho da imagem 
                path = Path.Combine(Server.MapPath("~/Imagens/"), nomeFoto);

                //e guardar nome do file na bd
                temporada.Foto = nomeFoto;
            }
            else
            {
                //caso contrário apresenta uma mensagem de erro
                ModelState.AddModelError("", "Não foi fornecida uma imagem...");
                ViewBag.SerieFK = temporada.SerieFK;

                return View(temporada);
            }
            //verifica se o modelo é válido
            if (ModelState.IsValid)
            {
                    temporada.Nome = temporada.Nome.Trim();
                //se o modelo for válido, adiciona 1 nova temporada á bd e guarda a foto
                db.Temporadas.Add(temporada);

                    
                    db.SaveChanges();
                    uploadFoto.SaveAs(path);
                    return RedirectToAction("Index",new { id = temporada.SerieFK});
                
                
                
            }}catch (Exception)
                {
                    //caso contrário apresenta uma mensagem de erro
                    ModelState.AddModelError("", "Não foi possivel guardar os dados. Por favor, tente novamente.");
                    ViewBag.SerieFK = temporada.SerieFK;
                    return View(temporada);
                }
            
            ViewBag.SerieFK = new SelectList(db.Series, "ID", "Nome", temporada.SerieFK);
            return View("Index", new { id = temporada.SerieFK });
        }


        //caso o utilizador seja do tipo "Administrador" , poderá fazer edit da temporada
        // GET: Temporadas/Edit/5
        /// <summary>
        /// faz get de todos os dados de 1 temporada
        /// </summary>
        /// <param name="id">id da temporada</param>
        /// <returns>view edit com os dados da temporada</returns>
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            //verifica se foi fornecido id
            if (id == null)
            {
                //caso não tenha sido fornecido nenhum id, o cliente é redirecionado para a página Home
                return Redirect("/");
            }
            //caso o id nao seja nulo, procura na bd a temporada com aquele id
            Temporadas temporada = db.Temporadas.Find(id);

            //verifica se o conteudo não é nulo
            if (temporada == null)
            {
                //caso seja o cliente é redirecionado para a página Home
                return Redirect("/");
            }
            ViewBag.SerieFK = new SelectList(db.Series, "ID", "Nome", temporada.SerieFK);
            return View(temporada);
        }

        // POST: Temporadas/Edit/5
        /// <summary>
        /// edita um registo de 1 temporada na bd incluindo a imagem associada . Devolve 1 mensagem de erro no caso de haver 1
        /// </summary>
        /// <param name="temporada">temporada (ID, Numero,Nome,Foto,Trailer,SerieFK)</param>
        /// <param name="editFoto">ficheiro de imagem</param>
        /// <returns>em caso de sucesso retona para a view index da mesma temporada, em caso de erro devolve 1 mensagem de erro</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "ID,Numero,Nome,Foto,Trailer,SerieFK")] Temporadas temporada, HttpPostedFileBase editFoto)
        {
            //conjunto de variáveis auxiliares para ajudar a guardar o caminho e o nome da foto
            string novoNome = "";
            string nomeAntigo = "";
            bool haFotoNova = false;
            string caminhoCompleto = "";

            //verificar se o modelo é válido
            if (ModelState.IsValid)
            {
                try
                {
                    //se for, verifica se o cliente forneceu uma imagem
                    if (editFoto != null)
                    {
                        //caso tenha fornecido, guarda o nome da foto
                        nomeAntigo = temporada.Foto;
                        novoNome = "Temporada_" + temporada.ID + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(editFoto.FileName).ToLower();
                        temporada.Foto = novoNome;
                        //variável para verificação da existencia de foto
                        haFotoNova = true;

                    }
                    temporada.Nome = temporada.Nome.Trim();
                    db.Entry(temporada).State = EntityState.Modified;
                    db.SaveChanges();

                    //caso haja foto, guarda o caminho da nova foto,elimina a foto antiga e guarda a foto nova
                    if (haFotoNova) {
                        caminhoCompleto = (Path.Combine(Server.MapPath("~/Imagens/"), nomeAntigo));
                        System.IO.File.Delete(caminhoCompleto);
                        editFoto.SaveAs(Path.Combine(Server.MapPath("~/Imagens/"), novoNome));
                    }
                    //caso contrário apresenta uma mensagem de erro
                } catch (Exception)
                {
                    ModelState.AddModelError("", string.Format("Ocorreu um erro com a edição da temporada,tente novamente.", temporada.Nome));
                }

            }
            ViewBag.SerieFK = new SelectList(db.Series, "ID", "Nome", temporada.SerieFK);
            return RedirectToAction("Index", new { id = temporada.SerieFK });
        }

        // GET: Temporadas/Delete/5
        /// <summary>
        /// faz get da temporada cujo id é o fornecido
        /// </summary>
        /// <param name="id">id da temporada</param>
        /// <returns>view delete com os dados da temporada</returns>
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return Redirect("/");
            }
            Temporadas temporadas = db.Temporadas.Find(id);
            if (temporadas == null)
            {
                return Redirect("/");
            }
            return View(temporadas);
        }

        // POST: Temporadas/Delete/5
        /// <summary>
        /// elimina o registo de 1 temporada cujo id foi o fornecido
        /// </summary>
        /// <param name="id">id da temporada</param>
        /// <returns>em caso de sucesso retorna para a view index da mesma serie onde estava, em caso de erro devolve uma mensagem de erro</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            //procura a temporada referente ao id fornecido
            Temporadas temporada = db.Temporadas.Find(id);
            if (temporada == null)
            {
                return Redirect("/");
            }
            try
            {
                //caso encontre e seja válida,remove o registo e guarda as alterações
                db.Temporadas.Remove(temporada);
                db.SaveChanges();
            }
            //caso contrário apresenta uma mensagem de erro
            catch (Exception)
            {
                ModelState.AddModelError("", string.Format("Não é possível apagar esta temporada pois existem episódios a ela associados."));
            }
            return View("Index", new { id = temporada.SerieFK });
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
