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
    public class PessoasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pessoas
        public ActionResult Index()
        {
            return View(db.Pessoas.ToList());
        }

        // GET: Pessoas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //alterar as rotas por defeito, de modo a não haver erros de BadRequest ou de NotFound  
                return RedirectToAction("Index");
            }
            Pessoas pessoas = db.Pessoas.Find(id);
            if (pessoas == null)
            {
                //alterar as rotas por defeito, de modo a não haver erros de BadRequest ou de NotFound  
                return RedirectToAction("Index");
            }
            var pap = pessoas.PessoasEpisodios.ToList();
            ViewBag.pap = pap;

            return View(pessoas);
        }

        // GET: Pessoas/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //o parametro serie recolhe os dados referentes a uma pessoa (Nome e o parametro fotografia representa a foto da pessoa)
        [Authorize(Roles = "Administrador")]
        public ActionResult Create([Bind(Include = "ID,Nome,Foto")] Pessoas pessoa,HttpPostedFileBase uploadFoto)
        {
            int idNovaPessoa = db.Pessoas.Max(s => s.ID) + 1;
            pessoa.ID = idNovaPessoa;

            string nomeFoto = "Pessoa_" + idNovaPessoa + ".jpg";

            string path = "";

            //verificar se foi fornecido ficheiro
            //Há ficheiro?
            if (uploadFoto != null)
            {
                // o ficheiro foi fornecido
                // validar se o q foi fornecido é uma imagem
                // criar o caminho completo até ao sítio onde o ficheiro
                // será guardado
                path = Path.Combine(Server.MapPath("~/Imagens/"), nomeFoto);

                //guardar nome do file na bd
                pessoa.Foto = nomeFoto;
            }
            else
            {
                ModelState.AddModelError("", "Não foi fornecida uma imagem...");

                return View(pessoa);
            }


            if (ModelState.IsValid)
            {
                // valida se os dados fornecidos estão de acordo 
                // com as regras definidas na especificação do Modelo
                //adiciona nova pessoa ao Modelo
                db.Pessoas.Add(pessoa);
                //guarda os dados na bd
                db.SaveChanges();
                //guarda a foto no disco rigido
                uploadFoto.SaveAs(path);
                return RedirectToAction("Index");
            }

            return View(pessoa);
        }

        // GET: Pessoas/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //alterar as rotas por defeito, de modo a não haver erros de BadRequest ou de NotFound  
                return RedirectToAction("Index");
            }
            Pessoas pessoas = db.Pessoas.Find(id);
            if (pessoas == null)
            {
                //alterar as rotas por defeito, de modo a não haver erros de BadRequest ou de NotFound  
                return RedirectToAction("Index");
            }
            return View(pessoas);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "ID,Nome,Foto")] Pessoas pessoa, HttpPostedFileBase editFoto)
        {
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
                        nomeAntigo = pessoa.Foto;
                        //o novo nome será guardado na bd se for inserida uma nova foto
                        novoNome = "Pessoa_" + pessoa.ID + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(editFoto.FileName).ToLower();
                        pessoa.Foto = novoNome;
                        haFotoNova = true;
                    }
                    db.Entry(pessoa).State = EntityState.Modified;
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
                    ModelState.AddModelError("", string.Format("Ocorreu um erro com a edição dos dados da pessoa {0}", pessoa.Nome));
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Pessoas/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //alterar as rotas por defeito, de modo a não haver erros de BadRequest ou de NotFound  
                return RedirectToAction("Index");
            }
            Pessoas pessoas = db.Pessoas.Find(id);
            if (pessoas == null)
            {
                //alterar as rotas por defeito, de modo a não haver erros de BadRequest ou de NotFound  
                return RedirectToAction("Index");
            }
            return View(pessoas);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            Pessoas pessoa = db.Pessoas.Find(id);
            try
            {
                //Remover uma serie
                //Caso haja papeos associados vai para a exceção
                db.Pessoas.Remove(pessoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", string.Format("Não é possível apagar esta pessoa pois existem papéis a ela associados"));
            }
            return View(pessoa);
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
