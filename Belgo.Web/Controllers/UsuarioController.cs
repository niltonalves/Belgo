using Belgo.Web.Models;
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
            var model = this.CarregarUsuario();
            return View(model);
        }
        public ActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Cadastrar(UsuarioModel model)
        {
            this.MostrarAlerta(TipoAlerta.Sucesso, "Usuário cadastrado com sucesso.");

            return RedirectToAction("Index");
        }
        public ActionResult Resetar(int id)
        {
            this.MostrarAlerta(TipoAlerta.Sucesso, "Senha resetada com sucesso. Nova senha <strong>XW304</strong>");

            return RedirectToAction("Index");
        }


        private List<UsuarioModel> CarregarUsuario()
        {
            var lista = new List<UsuarioModel>();
            for (int i = 1; i < 10; i++)
            {
                lista.Add(new UsuarioModel()
                {
                    ID = i,
                    Nome = "Teste " + i,
                    Email = "Teste" + i + "@teste.com.br",
                    DataCadastro = DateTime.Now,
                    DataUltimoAcesso = DateTime.Now.AddDays(-2),
                    Status = 1
                });
            }
            return lista;
        }

    }
}
