using Belgo.Dados.Entidade;
using Belgo.Dados.Modelo;
using Belgo.Dados.Util;
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
        public List<Pergunta> Listar()
        {
            try
            {
                var retorno = db.CAD_PERGUNTA.AsEnumerable().Select(p => Comum.TrataPergunta(p)).OrderBy(p => p.DataCriacao).ToList();

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
        public Pergunta Consultar(long id)
        {
            try
            {
                var retorno = db.CAD_PERGUNTA.AsEnumerable().Select(p => Comum.TrataPergunta(p)).FirstOrDefault(p => p.ID == id);
                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public CAD_PERGUNTA ConsultarPergunta(long id)
        {
            try
            {
                var retorno = db.CAD_PERGUNTA.FirstOrDefault(c => c.COD_PERGUNTA == id);
                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Cadastra a pergunta
        /// </summary>
        /// <param name="pesquisa">Objeto de pesquisa</param>
        /// <returns>ID da pesquisa cadastrada</returns>
        public long Cadastrar(Pergunta pergunta)
        {
            try
            {

                var ordem = db.CAD_PERGUNTA.Where(p => p.COD_PERGUNTA == pergunta.ID).Max(p => p.NUM_ORDEM_PERGUNTA) + 1;

                var cadastro = new CAD_PERGUNTA()
                {
                    COD_PESQUISA = pergunta.IdPesquisa,
                    DSC_PERGUNTA = pergunta.Descricao,
                    DTA_CRIACAO = pergunta.DataCriacao,
                    NUM_ORDEM_PERGUNTA = ordem,
                    IND_TPO_PERGUNTA = pergunta.Tipo,
                    COD_USER_CRIACAO = pergunta.IdUsuario
                };

                db.CAD_PERGUNTA.Add(cadastro);
                var retorno = db.SaveChanges();

                return cadastro.COD_PERGUNTA;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void Atualizar(Pergunta pergunta)
        {
            try
            {
                var cadastro = this.ConsultarPergunta(pergunta.ID);
                cadastro.DSC_PERGUNTA = pergunta.Descricao;
                cadastro.NUM_ORDEM_PERGUNTA = pergunta.Ordem;

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
                var cadastro = new CAD_PERGUNTA() { COD_PERGUNTA = id };
                db.Entry(cadastro).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
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
