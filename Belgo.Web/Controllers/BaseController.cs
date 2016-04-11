
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;
using System.Web.Mvc;
using System.Configuration;

namespace Belgo.Web.Controllers
{
    public class BaseController : Controller
    {
        public void MostrarAlerta(TipoAlerta tipo, string mensagem)
        {
            TempData[tipo.ToString()] = mensagem;
        }

        public enum TipoAlerta
        {
            Sucesso,
            Erro,
            Aviso
        }

        public void ExecutarApi()
        {
            string urlApi = ConfigurationManager.AppSettings["UrlApi"].ToString();
            var client = new RestClient(urlApi);

        }

    }
}
