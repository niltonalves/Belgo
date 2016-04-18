using Belgo.Web.Models;
using System.Text;
using System.Security.Cryptography;
using System.Web;

namespace Belgo.Web.Util
{
    public class Comum
    {
        public static UsuarioModel UsuarioLogado()
        {
            var usuario = (UsuarioModel)HttpContext.Current.Session["Usuario.Logado"];
            if (usuario == null)
                HttpContext.Current.Response.Redirect("~/Login/Sair");
            return usuario;

        }
        public static void GravarUsuarioLogado(UsuarioModel usuario)
        {
           HttpContext.Current.Session["Usuario.Logado"] = usuario;
        }

        public static string GerarHashMd5(string input)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
