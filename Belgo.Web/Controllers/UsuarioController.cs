using Belgo.Dados.Entidade;
using Belgo.Web.Models;
using Belgo.Web.Util;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Belgo.Web.Controllers
{
    public class UsuarioController : BaseController
    {

        public ActionResult Index()
        {
            try
            {
                var api = new RestApi();
                api.Method = Method.GET;
                api.Resource = RestApi.Resources.Usuario;
                api.Action = "listarativos";
                var lista = api.Executar<List<UsuarioModel>>();
                var usuarios = new List<UsuarioModel>();
                if (lista.Data != null)
                    usuarios = lista.Data;

                return View(usuarios);

            }
            catch (Exception)
            {

                throw;
            }

        }
        public ActionResult Cadastrar(long? id, bool? menu)
        {

            try
            {
                var usuario = new UsuarioModel();
                if (Convert.ToBoolean(menu))
                    id = Comum.UsuarioLogado().ID;

                if (id != null)
                    usuario = Consultar(Convert.ToInt64(id));


                return View(usuario);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPost]
        public ActionResult Cadastrar(UsuarioModel model)
        {
            try
            {
                if (ExisteCadastro(model.Email) && model.ID == 0)
                {
                    MostrarAlerta(TipoAlerta.Erro, "Email do usuário já cadastrado.");
                    return View(model);
                }

                var api = new RestApi();
                api.Method = Method.POST;
                api.Resource = RestApi.Resources.Usuario;
                var senha = model.SenhaAtual;
                var mensagem = "Usuário cadastrado com sucesso.";

                if (model.ID != 0)
                {
                    api.AdicionarParametro(new Parameter() { Type = ParameterType.UrlSegment, Name = "id", Value = model.ID });
                    api.AdicionarParametro(new Parameter() { Type = ParameterType.RequestBody, Name = "id", Value = model.ID });
                    mensagem = "Usuário alterado com sucesso.";
                }
                //efetua o cadastro ou a troca da senha
                if (model.Senha != model.SenhaAtual)
                    senha = Comum.GerarHashMd5(model.Senha);

                api.AdicionarParametro(new Parameter() { Type = ParameterType.RequestBody, Name = "Nome", Value = model.Nome });
                api.AdicionarParametro(new Parameter() { Type = ParameterType.RequestBody, Name = "Email", Value = model.Email });
                api.AdicionarParametro(new Parameter() { Type = ParameterType.RequestBody, Name = "Senha", Value = senha });

                api.Executar<long>();

                MostrarAlerta(TipoAlerta.Sucesso, mensagem);
                if (Request["menu"]!=null)
                {
                    MostrarAlerta(TipoAlerta.Sucesso, "Dados alterados com sucesso.");
                    return RedirectToAction("Cadastrar", new {menu = true });
                }

                
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }

        }
        public ActionResult Excluir(int id, bool? efetuar)
        {
            try
            {
                var model = new UsuarioModel() { ID = id };
                if (Convert.ToBoolean(efetuar))
                {
                    var api = new RestApi();
                    api.Method = Method.DELETE;
                    api.Resource = RestApi.Resources.Usuario;
                    api.AdicionarParametro(new Parameter() { Type = ParameterType.UrlSegment, Name = "id", Value = id });
                    api.Executar<long>();
                    MostrarAlerta(TipoAlerta.Sucesso, "Usuário deletado com sucesso");
                    return RedirectToAction("Index");
                }
                return PartialView("_Excluir", model);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult Resetar(int id)
        {
            this.MostrarAlerta(TipoAlerta.Sucesso, "Senha resetada com sucesso. Nova senha <strong>XW304</strong>");

            return RedirectToAction("Index");
        }

        private UsuarioModel Consultar(long id)
        {

            try
            {
                var usuario = new UsuarioModel();
                var api = new RestApi();
                api.Method = Method.GET;
                api.Resource = RestApi.Resources.Usuario;
                api.AdicionarParametro(new Parameter() { Type = ParameterType.UrlSegment, Name = "id", Value = id });
                var cadastro = api.Executar<UsuarioModel>();

                if (cadastro.Data != null)
                {
                    usuario = cadastro.Data;
                    usuario.SenhaAtual = usuario.Senha;
                }

                return usuario;

            }
            catch (Exception)
            {

                throw;

            }
        }

        private bool ExisteCadastro(string email)
        {
            try
            {
                var api = new RestApi();
                api.Method = Method.GET;
                api.Resource = RestApi.Resources.Usuario;
                api.Action = "consultarporemail";
                api.AdicionarParametro(new Parameter() { Type = ParameterType.UrlSegment, Name = "email", Value = email });
                var cadastro = api.Executar<UsuarioModel>();

                if (cadastro.Data != null)
                    return true;

                return false;

            }
            catch (Exception)
            {

                throw;
            }


        }




    }
}
