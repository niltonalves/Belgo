using Belgo.Dados.Modelo;
using Belgo.Data.Negocio;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Belgo.Api.Controllers
{
    public class PerguntaController : ApiController
    {
        PerguntaDados db = new PerguntaDados();

        [HttpGet]
        [Route("api/pergunta")]
        public List<CAD_PERGUNTA> GetAll()
        {
            var retorno = db.Listar();

            return retorno;
        }

        [HttpGet]
        [Route("api/pergunta/{id}")]
        public IHttpActionResult Get(int id)
        {
            var retorno = db.Consultar(id);
            if (retorno == null)
                return NotFound();

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/pergunta/{id}")]
        public IHttpActionResult Put(int id, [FromBody]CAD_PERGUNTA pergunta)
        {
            if (ModelState.IsValid)
            {
                this.db.Atualizar(pergunta);
                return Ok(HttpStatusCode.NoContent);
            }

            return Content(HttpStatusCode.BadRequest, "Erro de entrada");

        }

        [HttpPost]
        [Route("api/pergunta/")]
        public IHttpActionResult Post([FromBody]CAD_PERGUNTA pergunta)
        {
            if (pergunta == null)
                return Content(HttpStatusCode.BadRequest, "Erro de entrada");

            var retorno = this.db.Cadastrar(pergunta);
            return Ok(retorno);
        }

        //[HttpPost]
        //[Route("api/pergunta/{id}")]
        //public IHttpActionResult Delete(int id)
        //{
        //    db.Deletar(id);
        //    return Ok(HttpStatusCode.NoContent);
        //}

    }
}
