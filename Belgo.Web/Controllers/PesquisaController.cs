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

                pesquisaModel.Perguntas = pesquisa.Data.Perguntas.Select(p => new PesquisaModel.PerguntaModel()
                {
                    ID = p.ID,
                    Descricao = p.Descricao,
                    DataCriacao = p.DataCriacao,
                    Ordem = p.Ordem,
                    Tipo = p.Tipo,
                    Respostas = p.Respostas.Select(i => new RespostaModel() { ID = i.ID }).ToList()
                }).ToList();
                Session["Perguntas"] = pesquisaModel.Perguntas;

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

            this.MostrarAlerta(TipoAlerta.Sucesso, "Pesquisa cadastrada com sucesso.");

            if (model.ID != 0)
            {
                api.AdicionarParametro(new Parameter() { Type = ParameterType.UrlSegment, Name = "id", Value = model.ID });
                api.AdicionarParametro(new Parameter() { Type = ParameterType.RequestBody, Name = "ID", Value = model.ID });
                this.MostrarAlerta(TipoAlerta.Sucesso, "Pesquisa alterada com sucesso.");
            }

            var retorno = api.Executar<long>();

            return RedirectToAction("Cadastrar", new { id = retorno.Data });

        }
        public ActionResult Excluir(int id, bool? efetuar)
        {
            if (Convert.ToBoolean(efetuar))
            {
                var api = new RestApi();
                api.Method = Method.DELETE;
                api.Resource = RestApi.Resources.Pesquisa;
                api.AdicionarParametro(new Parameter() { Type = ParameterType.UrlSegment, Name = "id", Value = id });
                api.AdicionarParametro(new Parameter() { Type = ParameterType.UrlSegment, Name = "id", Value = id });
                var retorno = api.Executar<long>();

                this.MostrarAlerta(TipoAlerta.Sucesso, "Pesquisa excluída com sucesso.");
                return RedirectToAction("Index");
            }
            else
            {

                var model = new PesquisaModel()
                {
                    ID = id
                };
                return PartialView("_ExcluirPesquisa", model);

            }
        }

        public ActionResult CadastrarPergunta(long? id, long idPesquisa)
        {
            var model = new PerguntaModel();
            model.IdPesquisa = idPesquisa;

            if (id != null)
            {
                var api = new RestApi();
                api.Method = Method.GET;
                api.Resource = RestApi.Resources.Pergunta;
                api.AdicionarParametro(new Parameter() { Type = ParameterType.UrlSegment, Name = "id", Value = id });
                var retorno = api.Executar<Pergunta>();
                if (retorno.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    var _pergunta = retorno.Data;
                    model.ID = Convert.ToInt64(id);
                    model.Descricao = _pergunta.Descricao;
                    model.Ordem = _pergunta.Ordem;
                    model.DataCriacao = _pergunta.DataCriacao;
                    model.Tipo = _pergunta.Tipo;
                    model.IdPesquisa = _pergunta.IdPesquisa;
                    if (_pergunta.Respostas != null)
                        model.Respostas = _pergunta.Respostas.Select(r => new RespostaModel() { ID = r.ID, Descricao = r.Descricao, DataCriacao = r.DataCriacao, IdPergunta = r.IdPergunta }).ToList();

                    var lista = (List<PerguntaModel>)Session["Perguntas"];


                    ViewBag.Perguntas = lista.OrderBy(p => p.Descricao).ToList();
                }
            }

            //Tipos de perguntas
            this.TiposPerguntas();

            return View(model);
        }
        [HttpPost]
        public ActionResult CadastrarPergunta(PerguntaModel model)
        {
            var api = new RestApi();
            var usuario = Comum.UsuarioLogado();
            api.Method = Method.POST;
            api.Resource = RestApi.Resources.Pergunta;

            api.AdicionarParametro(new Parameter() { Type = ParameterType.RequestBody, Name = "Tipo", Value = model.Tipo });
            api.AdicionarParametro(new Parameter() { Type = ParameterType.RequestBody, Name = "IdPesquisa", Value = model.IdPesquisa });
            api.AdicionarParametro(new Parameter() { Type = ParameterType.RequestBody, Name = "IdUsuario", Value = usuario.ID });
            api.AdicionarParametro(new Parameter() { Type = ParameterType.RequestBody, Name = "Descricao", Value = model.Descricao });

            if (model.ID != 0)
            {
                api.AdicionarParametro(new Parameter() { Type = ParameterType.UrlSegment, Name = "id", Value = model.ID });
                api.AdicionarParametro(new Parameter() { Type = ParameterType.RequestBody, Name = "ID", Value = model.ID });

                this.MostrarAlerta(TipoAlerta.Sucesso, "Pergunta alterada com sucesso.");
            }
            else
            {
                this.MostrarAlerta(TipoAlerta.Sucesso, "Pergunta cadastrada com sucesso.");
            }

            var cadastro = api.Executar<long>();

            return RedirectToAction("CadastrarPergunta", new { id = cadastro.Data , idPesquisa =  model.IdPesquisa });
        }


        [HttpPost]
        public ActionResult Publicar(PesquisaModel model)
        {
            try
            {
                var api = new RestApi();
                var usuario = Comum.UsuarioLogado();
                api.Method = Method.POST;
                api.Resource = RestApi.Resources.Pesquisa;
                api.Action = "publicar";
                api.AdicionarParametro(new Parameter() { Type = ParameterType.UrlSegment, Name = "id", Value = model.ID });
                api.AdicionarParametro(new Parameter() { Type = ParameterType.RequestBody, Name = "ID", Value = model.ID });

                api.Executar<long>();

                this.MostrarAlerta(TipoAlerta.Sucesso, "Pesquisa publicada com sucesso.");
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return RedirectToAction("Index");
        }

        public ActionResult ExcluirPergunta(long id, long idPesquisa, bool? efetuar)
        {
            this.MostrarAlerta(TipoAlerta.Sucesso, "Pergunta excluída com sucesso.");

            if (Convert.ToBoolean(efetuar))
            {
                try
                {
                    var api = new RestApi();
                    api.Method = Method.DELETE;
                    api.Resource = RestApi.Resources.Pergunta;
                    api.AdicionarParametro(new Parameter() { Type = ParameterType.UrlSegment, Name = "id", Value = id });
                    api.Executar<Resposta>();
                    this.MostrarAlerta(TipoAlerta.Sucesso, "Pergunta excluída com sucesso.");
                    return RedirectToAction("Cadastrar", new { id = idPesquisa });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            var model = new PerguntaModel()
            {
                ID = id,
                IdPesquisa = idPesquisa
            };
            return PartialView("_ExcluirPergunta", model);

        }


        public ActionResult ExcluirResposta(long id, long idPergunta, long idPesquisa, bool? efetuar)
        {
            if (Convert.ToBoolean(efetuar))
            {
                try
                {
                    var api = new RestApi();
                    api.Method = Method.DELETE;
                    api.Resource = RestApi.Resources.Resposta;
                    api.AdicionarParametro(new Parameter() { Type = ParameterType.UrlSegment, Name = "id", Value = id });
                    api.Executar<Resposta>();

                    this.MostrarAlerta(TipoAlerta.Sucesso, "Resposta excluída com sucesso.");
                    return RedirectToAction("CadastrarPergunta", new { id = idPergunta, idPesquisa = idPesquisa });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            var model = new RespostaModel()
            {
                ID = id,
                IdPergunta = idPergunta,
                IdPesquisa = idPesquisa
            };
            return PartialView("_ExcluirResposta", model);
        }


        public ActionResult CadastrarResposta(long? id, long idPergunta, long? idPesquisa)
        {
            var model = new RespostaModel();
            model.IdPergunta = idPergunta;
            model.IdPesquisa = Convert.ToInt64(idPesquisa);

            if (id != null)
            {
                var api = new RestApi();
                api.Method = Method.GET;
                api.Resource = RestApi.Resources.Resposta;
                api.AdicionarParametro(new Parameter() { Type = ParameterType.UrlSegment, Name = "id", Value = id });
                var retorno = api.Executar<Resposta>();
                if (retorno.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    var _resposta = retorno.Data;
                    model.ID = Convert.ToInt64(id);
                    model.Descricao = _resposta.Descricao;
                    model.IdPergunta = _resposta.IdPergunta;
                    model.IdPesquisa = _resposta.IdPesquisa;
                }
            }
            return PartialView("_CadastrarResposta", model);
        }

        [HttpPost]
        public ActionResult CadastrarResposta(RespostaModel model)
        {
            try
            {
                var api = new RestApi();
                var usuario = Comum.UsuarioLogado();

                api.Method = Method.POST;
                api.Resource = RestApi.Resources.Resposta;
                api.AdicionarParametro(new Parameter() { Type = ParameterType.RequestBody, Name = "idPergunta", Value = model.IdPergunta });
                api.AdicionarParametro(new Parameter() { Type = ParameterType.RequestBody, Name = "Descricao", Value = model.Descricao });

                if (model.ID != 0)
                {
                    api.AdicionarParametro(new Parameter() { Type = ParameterType.UrlSegment, Name = "id", Value = model.ID });
                    api.AdicionarParametro(new Parameter() { Type = ParameterType.RequestBody, Name = "ID", Value = model.ID });
                    this.MostrarAlerta(TipoAlerta.Sucesso, "Resposta alterada com sucesso.");
                }
                else
                {
                    api.AdicionarParametro(new Parameter() { Type = ParameterType.RequestBody, Name = "idUsuarioCriacao", Value = usuario.ID });
                    this.MostrarAlerta(TipoAlerta.Sucesso, "Resposta cadastrada com sucesso.");

                }
                api.Executar<Resposta>();

            }
            catch (Exception ex)
            {


            }
            return RedirectToAction("CadastrarPergunta", new { id = model.IdPergunta, idPesquisa = model.IdPesquisa });
        }


        public ActionResult Visualizar(long id)
        {
            try
            {
                var api = new RestApi();
                api.Method = Method.GET;
                api.Resource = RestApi.Resources.Pesquisa;
                api.AdicionarParametro(new Parameter() { Type = ParameterType.UrlSegment, Name = "id", Value = id });
                var retorno = api.Executar<Pesquisa>();
                var pesquisaModel = new PesquisaModel()
                {
                    ID = retorno.Data.ID,
                    Nome = retorno.Data.Nome,
                    Fechado = retorno.Data.Fechado,
                    Perguntas = retorno.Data.Perguntas.Select(p => new PerguntaModel()
                    {
                        Ordem = p.Ordem,
                        Descricao = p.Descricao,
                        Tipo = p.Tipo,
                        Respostas = p.Respostas.Select(r => new RespostaModel() { Descricao = r.Descricao }).ToList()
                    }

                    ).OrderBy(p => p.Ordem).ToList()
                };
                return PartialView("_Visualizar", pesquisaModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

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
                Perguntas = p.Perguntas.Select(i => new PerguntaModel() { ID = i.ID }).ToList()

            }).ToList();


            return retorno;

        }

        private void TiposPerguntas()
        {
            var tiposPergunta = from Enumerador.TipoPergunta s in Enum.GetValues(typeof(Enumerador.TipoPergunta))
                                select new { ID = s, Name = Enumerador.GetDescription(s) };

            ViewBag.TiposPergunta = tiposPergunta;
        }


    }
}
