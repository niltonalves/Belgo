using Belgo.Dados.Entidade;
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
        public List<Pesquisa> GetAll()
        {
            var retorno = db.Listar(null);

            return retorno;
        }

        [HttpGet]
        [Route("api/pesquisa/fechado/{fechado}")]
        public List<Pesquisa> GetAllPublicados(string fechado)
        {
            var publicado = fechado.Equals("sim");
            var retorno = db.Listar(publicado);
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

        
        [HttpPost]
        [Route("api/pesquisa/{id}")]
        public IHttpActionResult Put(int id, [FromBody]Pesquisa pesquisa)
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
        public IHttpActionResult Post([FromBody]Pesquisa pesquisa)
        {
            if (pesquisa == null)
                return Content(HttpStatusCode.BadRequest, "Erro de entrada");
                    
            var retorno = this.db.Cadastrar(pesquisa);
            return Ok(retorno);
        }

        [HttpDelete]
        [Route("api/pesquisa/{id}")]
        public IHttpActionResult Delete(int id)
        {
            this.db.Deletar(id);

            return Ok(HttpStatusCode.NoContent);
        }

    }
}
