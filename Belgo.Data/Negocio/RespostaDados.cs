using Belgo.Dados.Entidade;
using Belgo.Dados.Modelo;
using Belgo.Dados.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Belgo.Data.Negocio
{

    public class RespostaDados : IDisposable
    {
        Entities db = new Entities();

        bool disposed = false;

        /// <summary>
        /// Lista as respostas cadastradas
        /// </summary>
        /// <returns>Lista de respostas</returns>
        public List<Resposta> Listar()
        {
            try
            {
                var retorno = db.CAD_RESPOSTA.AsEnumerable().Select(r => (Comum.TrataResposta(r))).
                    OrderBy(p => p.DataCriacao).ToList();

                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /// <summary>
        /// Realiza consulta da pesquisa
        /// </summary>
        /// <param name="id">ID da pesquisa</param>
        /// <returns>Objeto Pesquisa</returns>
        public Resposta Consultar(long id)
        {
            try
            {
                var retorno = db.CAD_RESPOSTA
                    .Include("CAD_PERGUNTA")
                    .AsEnumerable().
                    Select(r => (Comum.TrataResposta(r))).FirstOrDefault(r => r.ID == id);
                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public CAD_RESPOSTA ConsultarResposta(long id)
        {
            try
            {
                var retorno = db.CAD_RESPOSTA.FirstOrDefault(r => r.COD_RESPOSTA == id);
                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Cadastra a pesquisa
        /// </summary>
        /// <param name="pesquisa">Objeto de pesquisa</param>
        /// <returns>ID da pesquisa cadastrada</returns>
        public long Cadastrar(Resposta resposta)
        {
            try
            {
                var cadastro = new CAD_RESPOSTA()
                {
                    COD_PERGUNTA = resposta.IdPergunta,
                    DSC_RESPOSTA = resposta.Descricao,
                    DTA_CRIACAO = DateTime.Now,
                    COD_USER_CRIACAO = resposta.IdUsuarioCriacao
                };

                db.CAD_RESPOSTA.Add(cadastro);
                var retorno = db.SaveChanges();

                return cadastro.COD_PERGUNTA;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void Atualizar(Resposta resposta)
        {
            try
            {
                var cadastro = this.ConsultarResposta(resposta.ID);
                cadastro.DSC_RESPOSTA = resposta.Descricao;

                db.Entry(cadastro).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void Deletar(int id)
        {
            try
            {
                var cadastro = new CAD_RESPOSTA() { COD_RESPOSTA = id };
                db.Entry(cadastro).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        #region implementação de dispose
        ~RespostaDados()
        {
            Dispose(false);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }
        #endregion

    }
}
