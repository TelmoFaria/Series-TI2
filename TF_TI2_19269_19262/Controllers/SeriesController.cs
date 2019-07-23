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
        // cria VAR que representa a BD
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Series
        /// <summary>
        /// faz get de todas as séries e respetivas editoras
        /// </summary>
        /// <returns>devolve a view index com os dados das séries e das respetivas editoras</returns>
        public ActionResult Index()
        {
            // procura a totalidade das series na BD
            var series = db.Series.Include(s => s.Editora);
            return View(series.ToList());
        }

        // GET: Series/Details/5
        /// <summary>
        /// faz get dos dados de 1 série
        /// </summary>
        /// <param name="id">id da série</param>
        /// <returns>dados da série cujo id é o fonecido na view details</returns>
        public ActionResult Details(int? id)
        {

            if (id == null)
            { 
                //alterar as rotas por defeito, de modo a não haver erros de BadRequest ou de NotFound  
                return RedirectToAction("Index");
            }
            Series serie = db.Series.Find(id);
            if (serie == null)
            {
                return RedirectToAction("Index");
            }
            return View(serie);
        }

        // GET: Series/Temporadas/5
        /// <summary>
        /// get dos dados das temporadas associadas a 1 série para que posso ir para a página das temporadas
        /// </summary>
        /// <param name="id"></param>
        /// <returns>view das temporadas com os dados das temporadas associadas a 1 série</returns>
        public ActionResult Temporadas(int? id)
        {
            if (id == null)
            {
                //evitar erros de BadRequest e NotFound
                return RedirectToAction("Index");
            }
            var temporadas = db.Temporadas.Where(t => t.SerieFK == id);
            var varSerie = db.Series;
            if (temporadas == null)
            {
                return RedirectToAction("Index");
            }
            return View(temporadas);
        }
        //------------------------------------------------------------------------------------
        // GET: Series/Create
        /*
         * Apenas os utilizadores do tipo "Administrador" poderão criar, editar ou eliminar series
         */
         /// <summary>
         /// faz get dos dados de 1 série e poe os dados de 1 editora (id e nome ) num viewbag
         /// </summary>
         /// <returns>view create</returns>
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.EditoraFK = new SelectList(db.Editora, "ID", "Nome");
            return View();
        }

        // POST: Series/Create
        /// <summary>
        /// cria 1 registo de 1 série na bd incluindo o ficheiro de imagem.devolve mensagem de erro em caso de erro
        /// </summary>
        /// <param name="serie">série (Nome,genero,Sinopse,Video,AuxClassificacao e EditoraFK</param>
        /// <param name="uploadFoto">ficheiro de imagem</param>
        /// <returns>view create com os dados da série ou em caso de erro volta para o idex</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create([Bind(Include = "Nome,Genero,Sinopse,Video,AuxClassificacao,EditoraFK")] Series serie, HttpPostedFileBase uploadFoto)
        {
            try
            {
                //converter o auxClassificacao para double
                serie.Classificacao = Convert.ToDouble(serie.AuxClassificacao);
                int idNovaSerie = db.Series.Max(s => s.ID) + 1;
                serie.ID = idNovaSerie;

                string nomeFoto = "Serie_" + idNovaSerie + ".jpg";

                string path = "";
                //verificar se foi fornecido ficheiro
                //Há ficheiro?
                if (uploadFoto != null)
                {
                    // o ficheiro foi fornecido
                    // validar se o que foi fornecido é uma imagem
                    // criar o caminho completo até ao sítio onde o ficheiro
                    // será guardado
                    path = Path.Combine(Server.MapPath("~/Imagens/"), nomeFoto);

                    //guardar nome do file na bd
                    serie.Foto = nomeFoto;
                }
                //Não havendo ficheiro (e sendo obrigatório) vamos ter de fornecer uma imagem
                else
                {
                    //avisar que nao foi fornecida qualquer imagem
                    ModelState.AddModelError("", "Não foi fornecida uma imagem...");
                    ViewBag.EditoraFK = new SelectList(db.Editora, "ID", "Nome", serie.EditoraFK);

                    return View(serie);
                }

                //se os atributos introduzidos forem validos entra neste if 
                if (ModelState.IsValid)
                {
                    // valida se os dados fornecidos estão de acordo 
                    // com as regras definidas na especificação do Modelo
                    //adiciona nova serie ao Modelo
                    db.Series.Add(serie);
                    //guarda os dados na bd
                    db.SaveChanges();
                    //guarda a foto no disco rigido
                    uploadFoto.SaveAs(path);
                    // redireciona o utilizador para a página do INDEX
                    return RedirectToAction("Index");
                }

                ViewBag.EditoraFK = new SelectList(db.Editora, "ID", "Nome", serie.EditoraFK);
                return View(serie);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro com a criação dos dados da serie "));
                return View(serie);

            }
            
        }

        // GET: Series/Edit/5
        /// <summary>
        /// faz get dos dados de 1 série
        /// </summary>
        /// <param name="id">id da série</param>
        /// <returns> view edit com os dados da série</returns>
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //alterar as respostas por defeito, de modo a não haver erros de BadRequest ou de NotFound  
                return RedirectToAction("Index");
            }
            Series serie = db.Series.Find(id);
            if (serie == null)
            {
                //alterar as respostas por defeito, de modo a não haver erros de BadRequest ou de NotFound  
                return RedirectToAction("Index");
            }
         
            ViewBag.EditoraFK = new SelectList(db.Editora, "ID", "Nome", serie.ID);
            ViewBag.SerieID = new SelectList(db.Editora, "ID", "Nome", serie.ID);

            return View(serie);
        }

        // POST: Series/Edit/5
        /// <summary>
        /// Edita 1 registo de 1 série na bd incluindo o ficheiro de imagem associado.Devolve mensagem de erro em caso de erro 
        /// </summary>
        /// <param name="serie">série (ID, Nome,Genero,Foto,Sinopse,Video,AuxClassificacao,EditoraFK</param>
        /// <param name="editFoto">ficheiro de imagem</param>
        /// <returns>retorna para o series details com o id da série vindo do viewbag</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "ID,Nome,Genero,Foto,Sinopse,Video,AuxClassificacao,EditoraFK")] Series serie, HttpPostedFileBase editFoto)
        {
        //converter o atributo auxiliar para double
          serie.Classificacao=Convert.ToDouble(serie.AuxClassificacao);
            // o ficheiro foi fornecido
            // validar se o que foi fornecido é uma imagem
            // criar o caminho completo até ao sítio onde o ficheiro será guardado
            string novoNome = "";
            string nomeAntigo = "";
            bool haFotoNova = false;

            if (ModelState.IsValid)
            {
                 try
                 {
                    if (editFoto != null)
                    {
                        //o nome antigo representa a foto na base de dados já inserida
                        nomeAntigo = serie.Foto;
                        //o novo nome será guardado na bd se for inserida uma nova foto
                        novoNome = "Serie_" + serie.ID + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(editFoto.FileName).ToLower();
                        serie.Foto = novoNome;
                        haFotoNova = true;
                    }
                        db.Entry(serie).State = EntityState.Modified;
                        db.SaveChanges();
                        if (haFotoNova)
                            {
                                // eliminar a foto antiga da bd e guardar a nova foto na bd
                                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Imagens"), nomeAntigo));
                                editFoto.SaveAs(Path.Combine(Server.MapPath("~/Imagens"), novoNome));
                            }
                   }
                catch (Exception)
                {
                    //se houver um erro na edição apresenta este erro
                    ModelState.AddModelError("", string.Format("Ocorreu um erro com a edição dos dados da serie {0}", serie.Nome));
                }
            }
            ViewBag.EditoraFK = new SelectList(db.Editora, "ID", "Nome", serie.EditoraFK);
            return RedirectToAction("Details","Series",new { id= ViewBag.SerieID});

        }

        // GET: Series/Delete/5
        /// <summary>
        /// faz get dos dados da série cujo id é o fornecido 
        /// </summary>
        /// <param name="id">id da série</param>
        /// <returns>view delete com os dados da série</returns>
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //alterar as respostas por defeito, de modo a não haver erros de BadRequest ou de NotFound  
                return RedirectToAction("Index");
            }
            Series series = db.Series.Find(id);
            if (series == null)
            {
                //alterar as respostas por defeito, de modo a não haver erros de BadRequest ou de NotFound  
                return RedirectToAction("Index");
            }
            return View(series);
        }

        // POST: Series/Delete/5
        /// <summary>
        /// elimina o registo de 1 série da bd.Devolve mensagem de erro em caso de erro
        /// </summary>
        /// <param name="id">id de 1 série</param>
        /// <returns>retorna para a view index em caso de sucesso e retorna para a view delete com os dados da ´série em caso de falha com 1 mensagem de erro </returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            Series serie = db.Series.Find(id);
            try { 
            //Remover uma serie
            //Caso haja temporadas associadas vai para a exceção
            db.Series.Remove(serie);
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            //Se houver uma temporada associada à serie apresenta este erro.
            catch (Exception)
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
