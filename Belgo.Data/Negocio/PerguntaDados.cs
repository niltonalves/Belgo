using Belgo.Dados.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Belgo.Data.Negocio
{

    public class PerguntaDados : IDisposable
    {
        Entities db = new Entities();

        bool disposed = false;

        /// <summary>
        /// Lista as respostas cadastradas
        /// </summary>
        /// <returns>Lista de respostas</returns>
        public List<CAD_PERGUNTA> Listar()
        {
            try
            {
                var retorno = db.CAD_PERGUNTA.
                    Include("CAD_PESQUISA").
                    Include("CAD_RESPOSTA").
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
        public CAD_PERGUNTA Consultar(long id)
        {
            try
            {
                var retorno = db.CAD_PERGUNTA.
                    Include("CAD_PESQUISA").
                    Include("CAD_RESPOSTA").
                    FirstOrDefault(p => p.COD_PERGUNTA == id);
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
        public long Cadastrar(CAD_PERGUNTA pergunta)
        {
            try
            {
                pergunta.DTA_CRIACAO = DateTime.Now;
                db.CAD_PERGUNTA.Add(pergunta);
                var retorno = db.SaveChanges();

                return pergunta.COD_PERGUNTA;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void Atualizar(CAD_PERGUNTA pergunta)
        {
            try
            {
                var _uPergunta = this.Consultar(pergunta.COD_PERGUNTA);

                _uPergunta.DSC_PERGUNTA = pergunta.DSC_PERGUNTA;
                _uPergunta.NUM_ORDEM_PERGUNTA = Convert.ToInt32(pergunta.NUM_ORDEM_PERGUNTA);

                db.Entry(_uPergunta).State = System.Data.Entity.EntityState.Modified;
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
                var pergunta = this.Consultar(id);
                db.CAD_PERGUNTA.Remove(pergunta);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
       

        #region implementação de dispose
        ~PerguntaDados()
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
