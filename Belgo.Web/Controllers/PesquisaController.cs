using Belgo.Dados.Entidade;
using Belgo.Dados.Modelo;
using Belgo.Web.Models;
using Belgo.Web.Util;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Belgo.Web.Models.PesquisaModel;

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
            var pesquisaModel = new PesquisaModel();
            if (id != null)
            {
                var api = new RestApi();
                api.Method = Method.GET;
                api.Resource = RestApi.Resources.Pesquisa;
                api.AdicionarParametro(new Parameter() { Type = ParameterType.UrlSegment, Name = "id", Value = id });
                var pesquisa = api.Executar<Pesquisa>();

                pesquisaModel.ID = pesquisa.Data.ID;
                pesquisaModel.Nome = pesquisa.Data.Nome;

                pesquisaModel.Perguntas = pesquisa.Data.Perguntas.Select(p => new PesquisaModel.PerguntaModel() { ID = p.ID, Descricao = p.Descricao, DataCriacao = p.DataCriacao, Ordem = p.Ordem }).ToList();

                return View(pesquisaModel);
            }
            return View(pesquisaModel);

        }
        [HttpPost]
        public ActionResult Cadastrar(PesquisaModel model)
        {
            var usuario = Comum.UsuarioLogado();

            var api = new RestApi();
            api.Method = Method.POST;
            api.Resource = RestApi.Resources.Pesquisa;
            api.AdicionarParametro(new Parameter() { Type = ParameterType.RequestBody, Name = "Nome", Value = model.Nome });
            api.AdicionarParametro(new Parameter() { Type = ParameterType.RequestBody, Name = "IdUsuarioCriacao", Value = usuario.ID });
            if (model.ID != 0)
            {
                api.AdicionarParametro(new Parameter() { Type = ParameterType.UrlSegment, Name = "id", Value = model.ID });
                api.AdicionarParametro(new Parameter() { Type = ParameterType.RequestBody, Name = "ID", Value = model.ID });
            }
            api.Executar<Pesquisa>();

            return View(model);

        }
        public ActionResult Excluir(int id)
        {
            var api = new RestApi();
            api.Method = Method.GET;
            api.Resource = RestApi.Resources.Pesquisa;
            api.AdicionarParametro(new Parameter() { Type = ParameterType.UrlSegment, Name = "id", Value = id });
            api.Executar<Pesquisa>();

            this.MostrarAlerta(TipoAlerta.Sucesso, "Pesquisa excluída com sucesso.");
            return RedirectToAction("Index");

        }

        public ActionResult CadastrarPergunta()
        {
            var model = new PesquisaModel();
            //model.ID = 1;
            //model.Perguntas = this.CarregarResposta();
            return View(model);
        }
        [HttpPost]
        public ActionResult CadastrarPergunta(PesquisaModel model)
        {
            this.MostrarAlerta(TipoAlerta.Sucesso, "Pergunta cadastrada com sucesso.");
            return RedirectToAction("CadastrarPergunta", new { id = 1 });
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


        //private List<PesquisaModel.PerguntaModel> CarregarPergunta()
        //{
        //    var lista = new List<PesquisaModel.PerguntaModel>();
        //    for (int i = 1; i < 11; i++)
        //    {
        //        lista.Add(new Belgo.Web.Models.PesquisaModel.PerguntaModel()
        //        {
        //            ID = i,
        //            Titulo = "Título da pergunta " + i,
        //            DataCadastro = DateTime.Now.AddDays(-i)
        //        });
        //    }
        //    return lista;
        //}

        //private List<PesquisaModel.PerguntaModel> CarregarResposta()
        //{
        //    var lista = new List<PesquisaModel.PerguntaModel>();
        //    for (int i = 1; i < 11; i++)
        //    {
        //        lista.Add(new Belgo.Web.Models.PesquisaModel.PerguntaModel()
        //        {
        //            ID = i,
        //            Titulo = "Título da resposta " + i,
        //            DataCadastro = DateTime.Now.AddDays(-i)
        //        });
        //    }
        //    return lista;
        //}


        private List<PesquisaModel> CarregarPesquisa()
        {

            var api = new RestApi();
            api.Method = Method.GET;
            api.Resource = RestApi.Resources.Pesquisa;
            var lista = api.Executar<List<Pesquisa>>();

            var retorno = lista.Data.Select(p => new PesquisaModel
            {
                ID = p.ID,
                Nome = p.Nome,
                DataCriacao = p.DataCriacao,
                Fechado = p.Fechado,
                TotalPerguntas = p.Perguntas.Count()
                
            }).ToList();


            return retorno;

            //var lista = new List<PesquisaModel>();
            //for (int i = 1; i < 11; i++)
            //{
            //    lista.Add(new PesquisaModel()
            //    {
            //        ID = i,
            //        Nome = "Pesquisa Número " + i,
            //        DataCadastro = DateTime.Now.AddDays(-i),
            //        DataPublicacao = DateTime.Now.AddDays(i),
            //        Status = (i % 2 == 0 ? PesquisaModel.StatusPergunta.Publicado : PesquisaModel.StatusPergunta.Editando)
            //    });
            //}
            return null;
        }
        public void Api()

        {

        }

    }
}
