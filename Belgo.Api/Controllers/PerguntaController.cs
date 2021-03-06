﻿using Belgo.Dados.Entidade;
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
        public List<Pergunta> GetAll()
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
        public IHttpActionResult Put(int id, [FromBody]Pergunta pergunta)
        {
            if (ModelState.IsValid)
            {
                var retorno = db.Atualizar(pergunta);
                return Ok(retorno);
            }

            return Content(HttpStatusCode.BadRequest, "Erro de entrada");

        }

        [HttpPost]
        [Route("api/pergunta/")]
        public IHttpActionResult Post([FromBody]Pergunta pergunta)
        {
            if (pergunta == null)
                return Content(HttpStatusCode.BadRequest, "Erro de entrada");

            var retorno = db.Cadastrar(pergunta);
            return Ok(retorno);
        }

        [HttpDelete]
        [Route("api/pergunta/{id}")]
        public IHttpActionResult Delete(int id)
        {
            db.Deletar(id);
            return Ok(HttpStatusCode.NoContent);
        }

    }
}
