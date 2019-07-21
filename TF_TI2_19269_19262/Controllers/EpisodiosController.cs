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
    public class EpisodiosController : Controller
    {
        // cria VAR que representa a BD
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Episodios
        public ActionResult Index(int?id)
        {
            if (id == null)
            {
                //alterar as respostas por defeito, de modo a não haver erros de BadRequest ou de NotFound  
                return Redirect("/");
            }
            //Procura os episodios referentes a uma temporada (dada o seu id) para apresentar apenas esses episodios
            ViewBag.SerieID = db.Temporadas.Find(id).SerieFK;
            ViewBag.TemporadaID = id;
            var ep = from p in db.Episodios
                       where p.TemporadaFK == id
                       select p;
            return View(ep.ToList());
        }



        // GET: Episodios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //Alterar as rotas por defeito de modo a evitar as mensagens de BadRequest e NotFound
                return Redirect("/");
            }
            Episodios episodio = db.Episodios.Find(id);
            if (episodio == null)
            {
                return Redirect("/");
            }
            var coment = episodio.ListaDeComentarios.ToList();
            ViewBag.coment = coment;


            return View(episodio);
        }

        // GET: Episodios/Create
        /*
        * Apenas os utilizadores do tipo "Administrador" poderão criar, editar ou eliminar series
        */
        [Authorize(Roles = "Administrador")]
        public ActionResult Create(int id)
        {
            ViewBag.TemporadaFK = id;
            return View();
        }

        // POST: Episodios/Create
        //o parametro serie recolhe os dados referentes a um episodio (Nome, Numero, Sinopse, Trailer, AuxClassificacao (que mais tarde será substituido por classificacao e temporadaFK
        //e o parametro fotografia representa a foto do episodio
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create([Bind(Include = "Numero,Nome,Sinopse,Foto,Trailer,AuxClassificacao,TemporadaFK")] Episodios episodio, HttpPostedFileBase uploadFoto)
        {
            try
            {
                //Converter o AuxClassificacao para double
                episodio.Classificacao = Convert.ToDouble(episodio.AuxClassificacao);

                int idNovoEpisodio = db.Episodios.Max(t => t.ID) + 1;

                string nomeFoto = "Episodio_" + idNovoEpisodio + ".jpg";

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
                    episodio.Foto = nomeFoto;
                }
                else
                //Não havendo ficheiro (e sendo obrigatório) vamos ter de fornecer uma imagem
                {
                    //avisar que nao foi fornecida qualquer imagem
                    ModelState.AddModelError("", "Não foi fornecida uma imagem.");
                    ViewBag.TemporadaFK = new SelectList(db.Temporadas, "ID", "Nome", episodio.TemporadaFK);

                    return View(episodio);
                }
                if (ModelState.IsValid)
                {
                    // valida se os dados fornecidos estão de acordo 
                    // com as regras definidas na especificação do Modelo
                    //adiciona novo episodio ao Modelo
                    db.Episodios.Add(episodio);
                    //guarda os dados na bd
                    db.SaveChanges();
                    //guarda a foto no disco rigido
                    uploadFoto.SaveAs(path);
                    //tenho de passar para aqui o id que uso
                    return RedirectToAction("Index", new { id = episodio.TemporadaFK});
                }
            }
            catch
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro a Criar o Episódio"));
            }
            

            ViewBag.TemporadaFK = new SelectList(db.Temporadas, "ID", "Nome", episodio.TemporadaFK);
            return View(episodio);
        }

        // GET: Episodios/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //alterar as respostas por defeito, de modo a não haver erros de BadRequest ou de NotFound  
                return Redirect("/");
            }
            Episodios episodios = db.Episodios.Find(id);
            if (episodios == null)
            {
                //alterar as respostas por defeito, de modo a não haver erros de BadRequest ou de NotFound  
                return Redirect("/");
            }


            ViewBag.TemporadaFK = new SelectList(db.Temporadas, "ID", "Nome", episodios.TemporadaFK);
            return View(episodios);
        }

        // POST: Episodios/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "ID,Numero,Nome,Sinopse,Foto,Trailer,AuxClassificacao,TemporadaFK")] Episodios episodio, HttpPostedFileBase editFoto)
        {
            //converter o atributo auxiliar para double
            episodio.Classificacao = Convert.ToDouble(episodio.AuxClassificacao);
            // o ficheiro foi fornecido
            // validar se o que foi fornecido é uma imagem
            // criar o caminho completo até ao sítio onde o ficheiro será guardado
            string novoNome = "";
            string nomeAntigo = "";
            bool haFotoNova = false;
            string caminhoCompleto = "";

            if (ModelState.IsValid)
            {
                try
                {
                    if (editFoto != null)
                    {
                        //o nome antigo representa a foto na base de dados já inserida
                        nomeAntigo = episodio.Foto;
                        //o novo nome será guardado na bd se for inserida uma nova foto  
                        novoNome = "Episodio_" + episodio.ID + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(editFoto.FileName).ToLower();
                        episodio.Foto = novoNome;
                        haFotoNova = true;

                    }
                    db.Entry(episodio).State = EntityState.Modified;
                    db.SaveChanges();

                    if (haFotoNova)
                    {
                        caminhoCompleto = (Path.Combine(Server.MapPath("~/Imagens/"), nomeAntigo));
                        // eliminar a foto antiga da bd e guardar a nova foto na bd
                        System.IO.File.Delete(caminhoCompleto);
                        editFoto.SaveAs(Path.Combine(Server.MapPath("~/Imagens/"), novoNome));
                    }
                }
                catch (Exception ex)
                {
                    //se houver um erro na edição apresenta este erro
                    ModelState.AddModelError("", string.Format("Ocorreu um erro com a edição do episódio {0}", episodio.Nome));
                }

            }
           // ViewBag.TemporadaFK = new SelectList(db.Temporadas, "ID", "Nome", episodio.TemporadaFK);
            return RedirectToAction("Index", new { id = episodio.TemporadaFK });
        }

        // GET: Episodios/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //alterar as respostas por defeito, de modo a não haver erros de BadRequest ou de NotFound  
                return Redirect("/");
            }
            Episodios episodios = db.Episodios.Find(id);
            if (episodios == null)
            {
                //alterar as respostas por defeito, de modo a não haver erros de BadRequest ou de NotFound  
                return Redirect("/");
            }
            return View(episodios);
        }

        // POST: Episodios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            Episodios episodio = db.Episodios.Find(id);
            try
            {
                //Remover um episodio
                //Caso haja pessoas ou comentarios associadas vai para a exceção
                db.Episodios.Remove(episodio);
                db.SaveChanges();
            }
            //Se houver uma temporada associada à serie apresenta este erro.
            catch (Exception ex)
            {
                ModelState.AddModelError("", string.Format("Não é possível apagar este episodio pois existem comentários ou pessoas a ele associados"));
            }
            return RedirectToAction("Index", new { id = episodio.TemporadaFK });
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
