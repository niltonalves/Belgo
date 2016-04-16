using Belgo.Dados.Entidade;
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
        public List<Participacao> Listar()
        {
            try
            {
                var retorno = db.CAD_PARTICIPACAO
                    .Select(c => new Participacao()
                    {
                        ID = c.COD_PARTICIPACAO,
                        DataParticipacao = c.DTA_PARTICIPACAO.ToString(),
                        DataSincronizacao = c.DTA_SINCRONIZACAO.ToString(),
                        Descricao = c.DSC_RESPOSTA_DISSERTATIVA,
                        RespostaNula = c.IND_RESPOSTA_NULA,
                    }).ToList();

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
        public Participacao Consultar(long id)
        {
            try
            {

                var retorno = db.CAD_PARTICIPACAO.Where(c => c.COD_PARTICIPACAO == id)
                    .Select(c => new Participacao()
                    {
                        ID = c.COD_PARTICIPACAO,
                        DataParticipacao = c.DTA_PARTICIPACAO.ToString(),
                        DataSincronizacao = c.DTA_SINCRONIZACAO.ToString(),
                        Descricao = c.DSC_RESPOSTA_DISSERTATIVA,
                        RespostaNula = c.IND_RESPOSTA_NULA,
                    }).FirstOrDefault();

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
        public long Cadastrar(Participacao participacao)
        {
            try
            {

                var cadastro = new CAD_PARTICIPACAO()
                {
                    COD_RESPOSTA = participacao.IdResposta,
                    COD_PERGUNTA = participacao.IdPergunta,
                    DSC_RESPOSTA_DISSERTATIVA = participacao.Descricao,
                    IND_RESPOSTA_NULA = participacao.RespostaNula,
                    DTA_PARTICIPACAO = Convert.ToDateTime(participacao.DataParticipacao),
                    DTA_SINCRONIZACAO = Convert.ToDateTime(participacao.DataSincronizacao),
                };

                db.CAD_PARTICIPACAO.Add(cadastro);
                var retorno = db.SaveChanges();

                return cadastro.COD_PARTICIPACAO;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void Atualizar(Participacao participacao)
        {
            try
            {
                var cadastro = new CAD_PARTICIPACAO()
                {
                    COD_RESPOSTA = participacao.IdResposta,
                    COD_PERGUNTA = participacao.IdPergunta,
                    DSC_RESPOSTA_DISSERTATIVA = participacao.Descricao,
                    IND_RESPOSTA_NULA = participacao.RespostaNula,
                    DTA_PARTICIPACAO = Convert.ToDateTime(participacao.DataParticipacao),
                    DTA_SINCRONIZACAO = Convert.ToDateTime(participacao.DataSincronizacao)
                };

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
                var cadastro = new CAD_PARTICIPACAO() { COD_PARTICIPACAO = id };
                db.Entry(cadastro).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();

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
