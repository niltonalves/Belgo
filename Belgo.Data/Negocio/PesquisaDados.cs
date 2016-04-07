using Belgo.Dados.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Belgo.Data.Negocio
{

    public class PesquisaDados : IDisposable
    {
        Entities db = new Entities();

        bool disposed = false;

        /// <summary>
        /// Lista as pesquisas cadastradas
        /// </summary>
        /// <returns>Lista de pesquisas</returns>
        public List<CAD_PESQUISA> Listar()
        {
            try
            {
                var retorno = db.CAD_PESQUISA.
                    Include("CAD_PERGUNTA").
                    Include("CAD_PERGUNTA.CAD_RESPOSTA").
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
        public CAD_PESQUISA Consultar(long id)
        {
            try
            {
                var retorno = db.CAD_PESQUISA.
                    Include("CAD_PERGUNTA").
                    Include("CAD_PERGUNTA.CAD_RESPOSTA").
                    FirstOrDefault(p => p.COD_PESQUISA == id);
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
        public long Cadastrar(CAD_PESQUISA pesquisa)
        {
            try
            {
                pesquisa.DTA_CRIACAO = DateTime.Now;
                pesquisa.IND_FECHADO = Convert.ToBoolean(pesquisa.IND_FECHADO);
                db.CAD_PESQUISA.Add(pesquisa);
                var retorno = db.SaveChanges();

                return pesquisa.COD_PESQUISA;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void Atualizar(CAD_PESQUISA pesquisa)
        {
            try
            {
                var _uPesquisa = this.Consultar(pesquisa.COD_PESQUISA);

                _uPesquisa.NOM_PESQUISA = pesquisa.NOM_PESQUISA;
                _uPesquisa.IND_FECHADO = Convert.ToBoolean(pesquisa.IND_FECHADO);

                db.Entry(_uPesquisa).State = System.Data.Entity.EntityState.Modified;
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
                var pesquisa = this.Consultar(id);
                db.CAD_PESQUISA.Remove(pesquisa);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void Publicar(int id)
        {
            try
            {
                var pesquisa = this.Consultar(id);
                pesquisa.IND_FECHADO = true;

                db.Entry(pesquisa);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }


        #region implementação de dispose
        ~PesquisaDados()
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
