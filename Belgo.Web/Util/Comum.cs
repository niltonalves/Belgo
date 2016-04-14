using Belgo.Web.Models;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Belgo.Web.Util
{
    public class Comum
    {
        public static UsuarioModel UsuarioLogado()
        {
            var usuario = new UsuarioModel() { ID = 1, Nome = "Admin" };
            return usuario;

        }


    }
}
