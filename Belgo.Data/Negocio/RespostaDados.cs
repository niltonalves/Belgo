using Belgo.Dados.Modelo;
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
        public List<CAD_RESPOSTA> Listar()
        {
            try
            {
                var retorno = db.CAD_RESPOSTA.
                    Include("CAD_PERGUNTA").
                    Include("CAD_PERGUNTA.CAD_PESQUISA").
                    OrderBy(p => p.DTA_CRIACAO).ToList();

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
        public CAD_RESPOSTA Consultar(long id)
        {
            try
            {
                var retorno = db.CAD_RESPOSTA.
                    Include("CAD_PERGUNTA").
                    Include("CAD_PERGUNTA.CAD_PESQUISA").
                    FirstOrDefault(p => p.COD_RESPOSTA == id);
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
        public long Cadastrar(CAD_RESPOSTA resposta)
        {
            try
            {
                resposta.DTA_CRIACAO = DateTime.Now;
                db.CAD_RESPOSTA.Add(resposta);
                var retorno = db.SaveChanges();

                return resposta.COD_PERGUNTA;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void Atualizar(CAD_RESPOSTA resposta)
        {
            try
            {
                var _uResposta = this.Consultar(resposta.COD_RESPOSTA);

                _uResposta.DSC_RESPOSTA = resposta.DSC_RESPOSTA;
                
                db.Entry(_uResposta).State = System.Data.Entity.EntityState.Modified;
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
                var resposta = this.Consultar(id);
                db.CAD_RESPOSTA.Remove(resposta);
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
