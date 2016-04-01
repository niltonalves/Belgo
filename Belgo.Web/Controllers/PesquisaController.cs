using Belgo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Belgo.Web.Controllers
{
    public class PesquisaController : BaseController
    {

        public ActionResult Index()
        {
            var lista = this.CarregarPesquisa();
            return View(lista);
        }
        public ActionResult Cadastrar(int? id)
        {
            var model = new PesquisaModel();
            if (id != null)
            {
                model.ID = Convert.ToInt32(id);
                model.Nome = "Teste";
                model.Perguntas = this.CarregarPergunta();
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult Cadastrar(PesquisaModel model)
        {
            model.ID = 1;
            model.Perguntas = this.CarregarPergunta();
            return View(model);
        }
        public ActionResult CadastrarPergunta()
        {
            var model = new PesquisaModel();
            model.ID = 1;
            model.Perguntas = this.CarregarResposta();
            return View(model);
        }
        [HttpPost]
        public ActionResult CadastrarPergunta(PesquisaModel model)
        {
            this.MostrarAlerta(TipoAlerta.Sucesso, "Pergunta cadastrada com sucesso.");
            return RedirectToAction("CadastrarPergunta", new {id = 1});
        }
        [HttpPost]
        public ActionResult Publicar(PesquisaModel model)
        {
            this.MostrarAlerta(TipoAlerta.Sucesso, "Pesquisa publicada com sucesso.");
            return RedirectToAction("Index");
        }

        public ActionResult ExcluirPergunta() 
        {
            this.MostrarAlerta(TipoAlerta.Sucesso, "Pergunta excluída com sucesso.");
            return RedirectToAction("Cadastrar", new { id = 1 });
        }


        private List<PesquisaModel.PerguntaModel> CarregarPergunta()
        {
            var lista = new List<PesquisaModel.PerguntaModel>();
            for (int i = 1; i < 11; i++)
            {
                lista.Add(new Belgo.Web.Models.PesquisaModel.PerguntaModel()
                {
                    ID = i,
                    Titulo = "Título da pergunta " + i,
                    DataCadastro = DateTime.Now.AddDays(-i)
                });
            }
            return lista;
        }

        private List<PesquisaModel.PerguntaModel> CarregarResposta()
        {
            var lista = new List<PesquisaModel.PerguntaModel>();
            for (int i = 1; i < 11; i++)
            {
                lista.Add(new Belgo.Web.Models.PesquisaModel.PerguntaModel()
                {
                    ID = i,
                    Titulo = "Título da resposta " + i,
                    DataCadastro = DateTime.Now.AddDays(-i)
                });
            }
            return lista;
        }


        private List<PesquisaModel> CarregarPesquisa()
        {
            var lista = new List<PesquisaModel>();
            for (int i = 1; i < 11; i++)
            {
                lista.Add(new PesquisaModel()
                {
                    ID = i,
                    Nome = "Pesquisa Número " + i,
                    DataCadastro = DateTime.Now.AddDays(-i),
                    DataPublicacao = DateTime.Now.AddDays(i),
                    Status = (i % 2 == 0 ? PesquisaModel.StatusPergunta.Publicado : PesquisaModel.StatusPergunta.Editando)
                });
            }
            return lista;
        }


    }
}
