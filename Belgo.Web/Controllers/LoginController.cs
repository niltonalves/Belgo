using Belgo.Web.Models;
using Belgo.Web.Util;
using RestSharp;
using System.Web.Mvc;
using System.Web.Security;
using static Belgo.Web.Controllers.BaseController;

namespace Belgo.Web.Controllers
{
    public class LoginController : BaseController
    {

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            var usuario = Autenticar(model.Email, model.Senha);
            if (usuario!=null)
            {
                FormsAuthentication.SetAuthCookie(model.Email, false);
                Comum.GravarUsuarioLogado(usuario);
                return RedirectToAction("Index", "Home");
            }

            MostrarAlerta(TipoAlerta.Erro, "Dados de acesso incorretos.");
            return View(model);
        }
        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        public UsuarioModel Autenticar(string email, string senha)
        {
            try
            {
                var api = new RestApi();
                api.Method = Method.POST;
                api.Resource = RestApi.Resources.Usuario;
                api.Action = "autenticar";
                api.AdicionarParametro(new Parameter() { Type = ParameterType.RequestBody, Name = "Email", Value = email});
                api.AdicionarParametro(new Parameter() { Type = ParameterType.RequestBody, Name = "Senha", Value = Comum.GerarHashMd5(senha)});
                var retorno = api.Executar<UsuarioModel>();
                return retorno.Data;

            }
            catch (System.Exception)
            {

                throw;
            }


        }
    }
}
