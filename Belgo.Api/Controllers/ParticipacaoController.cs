using Belgo.Dados.Modelo;
using Belgo.Data.Negocio;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Belgo.Api.Controllers
{
    public class ParticipacaoController : ApiController
    {
        ParticipacaoDados db = new ParticipacaoDados();

        [HttpGet]
        [Route("api/participacao")]
        public List<CAD_PARTICIPACAO> GetAll()
        {
            var retorno = db.Listar();

            return retorno;
        }

        [HttpGet]
        [Route("api/participacao/{id}")]
        public IHttpActionResult Get(int id)
        {
            var retorno = db.Consultar(id);
            if (retorno == null)
                return NotFound();

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/participacao/{id}")]
        public IHttpActionResult Put(int id, [FromBody]CAD_PARTICIPACAO participacao)
        {
            if (ModelState.IsValid)
            {
                this.db.Atualizar(participacao);
                return Ok(HttpStatusCode.NoContent);
            }

            return Content(HttpStatusCode.BadRequest, "Erro de entrada");

        }

        [HttpPost]
        [Route("api/participacao/")]
        public IHttpActionResult Post([FromBody]CAD_PARTICIPACAO participacao)
        {
            if (participacao == null)
                return Content(HttpStatusCode.BadRequest, "Erro de entrada");

            var retorno = this.db.Cadastrar(participacao);
            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/participacao/{id}")]
        public IHttpActionResult Delete(int id)
        {
            db.Deletar(id);
            return Ok(HttpStatusCode.NoContent);
        }

       
    }
}
