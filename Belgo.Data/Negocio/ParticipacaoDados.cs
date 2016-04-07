using Belgo.Dados.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Belgo.Data.Negocio
{

    public class ParticipacaoDados : IDisposable
    {
        Entities db = new Entities();

        bool disposed = false;

        /// <summary>
        /// Lista as respostas cadastradas
        /// </summary>
        /// <returns>Lista de respostas</returns>
        public List<CAD_PARTICIPACAO> Listar()
        {
            try
            {
                var retorno = db.CAD_PARTICIPACAO.
                    Include("CAD_RESPOSTA").
                    Include("CAD_RESPOSTA.CAD_PERGUNTA").
                    OrderBy(p => p.DTA_PARTICIPACAO).ToList();

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
        public CAD_PARTICIPACAO Consultar(long id)
        {
            try
            {
                var retorno = db.CAD_PARTICIPACAO.
                    Include("CAD_RESPOSTA").
                    Include("CAD_RESPOSTA.CAD_PERGUNTA").
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
        public long Cadastrar(CAD_PARTICIPACAO participacao)
        {
            try
            {
                participacao.DTA_PARTICIPACAO = (participacao.DTA_PARTICIPACAO == null ?  DateTime.Now : participacao.DTA_PARTICIPACAO);
                participacao.DTA_SINCRONIZACAO = DateTime.Now;
                db.CAD_PARTICIPACAO.Add(participacao);
                var retorno = db.SaveChanges();

                return participacao.COD_PARTICIPACAO;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void Atualizar(CAD_PARTICIPACAO participacao)
        {
            try
            {
                var _uParticipacao = this.Consultar(participacao.COD_PARTICIPACAO);

                _uParticipacao.DSC_RESPOSTA_DISSERTATIVA = participacao.DSC_RESPOSTA_DISSERTATIVA;
                _uParticipacao.IND_RESPOSTA_NULA = participacao.IND_RESPOSTA_NULA;

                db.Entry(_uParticipacao).State = System.Data.Entity.EntityState.Modified;
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
                var participacao = this.Consultar(id);
                db.CAD_PARTICIPACAO.Remove(participacao);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        #region implementação de dispose
        ~ParticipacaoDados()
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
