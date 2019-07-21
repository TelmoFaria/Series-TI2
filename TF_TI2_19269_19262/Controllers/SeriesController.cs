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
        public ActionResult Index()
        {
            // procura a totalidade das series na BD
            var series = db.Series.Include(s => s.Editora);
            return View(series.ToList());
        }

        // GET: Series/Details/5
        /*
         mostra os dados referentes a uma serie
         */
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
        //------------------------------------------------------------------------------------
        //                             Tentar ir para a pagina Temporadas 
        // GET: Series/Temporadas/5
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
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.EditoraFK = new SelectList(db.Editora, "ID", "Nome");
            return View();
        }

        // POST: Series/Create

        //o parametro serie recolhe os dados referentes a uma serie (Nome, Genero, Sinopse, Video, AuxClassificacao (que mais tarde será substituido por classificacao e editoraFK
        //e o parametro fotografia representa a foto da serie
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
            catch (Exception ex)
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
