using Belgo.Dados.Modelo;
using Belgo.Data.Negocio;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Belgo.Api.Controllers
{
    public class RespostaController : ApiController
    {
        RespostaDados db = new RespostaDados();

        [HttpGet]
        [Route("api/resposta")]
        public List<CAD_RESPOSTA> GetAll()
        {
            var retorno = db.Listar();

            return retorno;
        }

        [HttpGet]
        [Route("api/resposta/{id}")]
        public IHttpActionResult Get(int id)
        {
            var retorno = db.Consultar(id);
            if (retorno == null)
                return NotFound();

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/resposta/{id}")]
        public IHttpActionResult Put(int id, [FromBody]CAD_RESPOSTA resposta)
        {
            if (ModelState.IsValid)
            {
                this.db.Atualizar(resposta);
                return Ok(HttpStatusCode.NoContent);
            }

            return Content(HttpStatusCode.BadRequest, "Erro de entrada");

        }

        [HttpPost]
        [Route("api/resposta/")]
        public IHttpActionResult Post([FromBody]CAD_RESPOSTA resposta)
        {
            if (resposta == null)
                return Content(HttpStatusCode.BadRequest, "Erro de entrada");

            var retorno = this.db.Cadastrar(resposta);
            return Ok(retorno);
        }

        //[HttpPost]
        //[Route("api/resposta/{id}")]
        //public IHttpActionResult Delete(int id)
        //{
        //    db.Deletar(id);
        //    return Ok(HttpStatusCode.NoContent);
        //}

    }
}
