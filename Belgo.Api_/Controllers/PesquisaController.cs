using Belgo.Dados.Modelo;
using Belgo.Data.Negocio;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Belgo.Api.Controllers
{
    public class PesquisaController : ApiController
    {
        PesquisaDados db = new PesquisaDados();

        [HttpGet]
        [Route("api/pesquisa")]
        public List<CAD_PESQUISA> GetAll()
        {
            var retorno = db.Listar();

            return retorno;
        }

        [HttpGet]
        [Route("api/pesquisa/{id}")]
        public IHttpActionResult Get(int id)
        {
            var retorno = db.Consultar(id);
            if (retorno == null)
                return NotFound();

            return Ok(retorno);
        }


        [Route("api/pesquisa/{id}")]
        [HttpPost]
        public IHttpActionResult Put(int id, [FromBody]CAD_PESQUISA pesquisa)
        {
            if (ModelState.IsValid)
            {
                this.db.Atualizar(pesquisa);
                return Ok(HttpStatusCode.NoContent);
            }

            return Content(HttpStatusCode.BadRequest, "Erro de entrada");

        }

        [HttpPost]
        [Route("api/pesquisa/")]
        public IHttpActionResult Post([FromBody]CAD_PESQUISA pesquisa)
        {
            if (pesquisa == null)
                return Content(HttpStatusCode.BadRequest, "Erro de entrada");
                    
            var retorno = this.db.Cadastrar(pesquisa);
            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/pesquisa/")]
        public IHttpActionResult Delete(int id)
        {

            return Ok(HttpStatusCode.NoContent);
        }

    }
}
