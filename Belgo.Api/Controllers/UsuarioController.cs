using Belgo.Dados.Entidade;
using Belgo.Dados.Negocio;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace Belgo.Api.Controllers
{
    public class UsuarioController : ApiController
    {
        UsuarioDados db = new UsuarioDados();

        [Route("api/usuario/listarativos")]
        public List<Usuario> GetAllAtivos()
        {
            var retorno = db.Listar(false);

            return retorno;
        }
        [Route("api/usuario/listarinativos")]
        public List<Usuario> GetAllInativos()
        {
            var retorno = db.Listar(true);

            return retorno;
        }

        [HttpGet]
        [Route("api/usuario/{id}")]
        public IHttpActionResult Get(int id)
        {
            var retorno = db.Consultar(id);
            if (retorno == null)
                return NotFound();

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/usuario/consultarporemail/{email}")]
        public IHttpActionResult GetPorEmail(string email)
        {
            var retorno = db.Consultar(email);
            if (retorno == null)
                return NotFound();

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/usuario/{id}")]
        public IHttpActionResult Put(int id, [FromBody]Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                this.db.Atualizar(usuario);
                return Ok(HttpStatusCode.NoContent);
            }

            return Content(HttpStatusCode.BadRequest, "Erro de entrada");

        }


        [HttpPost]
        [Route("api/usuario/")]
        public IHttpActionResult Post([FromBody]Usuario usuario)
        {
            if (usuario == null)
                return Content(HttpStatusCode.BadRequest, "Erro de entrada");

            var retorno = db.Cadastrar(usuario);
            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/usuario/autenticar")]
        public IHttpActionResult PostAutenticar([FromBody]Usuario usuario)
        {
            if (usuario == null)
                return Content(HttpStatusCode.BadRequest, "Erro de entrada");

            var retorno = db.Autenticar(usuario);
            return Ok(retorno);
        }


        [HttpDelete]
        [Route("api/usuario/{id}")]
        public IHttpActionResult Delete(int id)
        {
            this.db.Deletar(id);
            return Ok(HttpStatusCode.NoContent);
        }

    }
}
