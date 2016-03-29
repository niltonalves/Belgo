using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}
