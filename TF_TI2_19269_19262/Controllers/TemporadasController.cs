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
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return Redirect("/");
            }
            var temporada = db.Temporadas.Include(t => t.Serie);
            var temp = from t in db.Temporadas
                       where t.SerieFK == id
                         select t;
            return View(temp.ToList());
        }
        // GET: Temporadas/select{id}
        public ActionResult Temp(int id)
        {
            
            var result = from r in db.Temporadas
                         where r.SerieFK == id
                         select r;
            return View(result);
        }

        // GET: Temporadas/Details/5
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
        

        // GET: Temporadas/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.SerieFK = new SelectList(db.Series, "ID", "Nome");
            return View();
        }
        //---
        // POST: Temporadas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create([Bind(Include = "ID,Numero,Nome,Trailer,SerieFK")] Temporadas temporada, HttpPostedFileBase uploadFoto)
        {

            /*
             para fazer o post create de uma temporada , são recebidos os valores de ID, Numero.Nome,Trailer, SerieFK que representam uma temporada, e 1 foto
             */
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
                ViewBag.SerieFK = new SelectList(db.Series, "ID", "Nome", temporada.SerieFK);

                return View(temporada);
            }
            //verifica se o modelo é válido
            if (ModelState.IsValid)
            {
                //se o modelo for válido, adiciona 1 nova temporada á bd e guarda a foto
                db.Temporadas.Add(temporada);
                db.SaveChanges();
                uploadFoto.SaveAs(path);
                return RedirectToAction("Index");
            }
            
            ViewBag.SerieFK = new SelectList(db.Series, "ID", "Nome", temporada.SerieFK);
            return View(temporada);
        }


        //caso o utilizador seja do tipo "Administrador" , poderá fazer edit da temporada
        // GET: Temporadas/Edit/5
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
                    db.Entry(temporada).State = EntityState.Modified;
                    db.SaveChanges();

                    //caso haja foto, guarda o caminho da nova foto,elimina a foto antiga e guarda a foto nova
                    if (haFotoNova) {
                        caminhoCompleto = (Path.Combine(Server.MapPath("~/Imagens/"), nomeAntigo));
                        System.IO.File.Delete(caminhoCompleto);
                        editFoto.SaveAs(Path.Combine(Server.MapPath("~/Imagens/"), novoNome));
                    }
                    //caso contrário apresenta uma mensagem de erro
                } catch (Exception ex)
                {
                    ModelState.AddModelError("", string.Format("Ocorreu um erro com a edição da temporada {0}", temporada.Nome));
                }

            }
            ViewBag.SerieFK = new SelectList(db.Series, "ID", "Nome", temporada.SerieFK);
            return RedirectToAction("Index");
        }

        // GET: Temporadas/Delete/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            //procura a temporada referente ao id fornecido
            Temporadas temporada = db.Temporadas.Find(id);
            try
            {
                //caso encontre e seja válida,remove o registo e guarda as alterações
                db.Temporadas.Remove(temporada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //caso contrário apresenta uma mensagem de erro
            catch (Exception ex)
            {
                ModelState.AddModelError("", string.Format("Não é possível apagar esta temporada pois existem episódios a ela associados"));
            }
            return View(temporada);
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
