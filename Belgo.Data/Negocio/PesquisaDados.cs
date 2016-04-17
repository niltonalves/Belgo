using Belgo.Dados.Entidade;
using Belgo.Dados.Modelo;
using Belgo.Dados.Util;
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
        public List<Pesquisa> Listar(bool? fechado)
        {
            try
            {

                var retorno = (from p in db.CAD_PESQUISA
                              .Include("CAD_PERGUNTA")
                               .Include("CAD_PERGUNTA.CAD_RESPOSTA")
                               select p).AsEnumerable()
                              .Select(a => new Pesquisa
                              {
                                  ID = a.COD_PESQUISA,
                                  Nome = a.NOM_PESQUISA,
                                  Fechado = a.IND_FECHADO ?? false,
                                  DataCriacao = a.DTA_CRIACAO,
                                  Perguntas = a.CAD_PERGUNTA.Select(b => new Pergunta(Comum.TrataPergunta(b))
                                  {
                                      Respostas = b.CAD_RESPOSTA.Select(c => (Comum.TrataResposta(c))).ToList()

                                  }).OrderBy(c => c.Ordem).ToList()
                              }).OrderBy(p => p.DataCriacao).ToList();

                if (fechado != null)
                    retorno = retorno.Where(c => c.Fechado == fechado).ToList();

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
        public Pesquisa Consultar(long id)
        {
            try
            {
                var retorno = (from p in db.CAD_PESQUISA
                              .Include("CAD_PERGUNTA")
                               .Include("CAD_PERGUNTA.CAD_RESPOSTA")
                               select p).AsEnumerable()
                              .Select(a => new Pesquisa
                              {
                                  ID = a.COD_PESQUISA,
                                  Nome = a.NOM_PESQUISA,
                                  Fechado = a.IND_FECHADO ?? false,
                                  DataCriacao = a.DTA_CRIACAO,
                                  Perguntas = a.CAD_PERGUNTA.Select(b => new Pergunta(Comum.TrataPergunta(b))
                                  {
                                      Respostas = b.CAD_RESPOSTA.Select(c => (Comum.TrataResposta(c))).ToList()

                                  }).OrderBy(c => c.Ordem).ToList()
                              }).FirstOrDefault(c => c.ID == id);
                return retorno;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private CAD_PESQUISA ConsultarPesquisa(long id)
        {
            try
            {
                var retorno = db.CAD_PESQUISA.SingleOrDefault(c => c.COD_PESQUISA == id);
                return retorno;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Cadastra a pesquisa
        /// </summary>
        /// <param name="pesquisa">Objeto de pesquisa</param>
        /// <returns>ID da pesquisa cadastrada</returns>
        public long Cadastrar(Pesquisa pesquisa)
        {
            try
            {
                var cadastro = new CAD_PESQUISA()
                {
                    NOM_PESQUISA = pesquisa.Nome,
                    DTA_CRIACAO = DateTime.Now,
                    COD_USER_CRIACAO = pesquisa.IdUsuarioCriacao,
                    IND_FECHADO = Convert.ToBoolean(pesquisa.Fechado)
                };

                db.CAD_PESQUISA.Add(cadastro);
                db.SaveChanges();
                return cadastro.COD_PESQUISA;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public long Atualizar(Pesquisa pesquisa)
        {
            try
            {
                var cadastro = this.ConsultarPesquisa(pesquisa.ID);
                cadastro.NOM_PESQUISA = pesquisa.Nome;

                db.Entry(cadastro).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return pesquisa.ID;

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
                var pesquisa = new CAD_PESQUISA() { COD_PESQUISA = id };
                db.Entry(pesquisa).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();

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
                var cadastro = this.ConsultarPesquisa(id);
                cadastro.IND_FECHADO = true;

                db.Entry(cadastro).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public Pesquisa RelatorioParticipacao(long id)
        {
            try
            {
                var retorno = (from p in db.CAD_PESQUISA
                                               .Include("CAD_PERGUNTA")
                                              .Include("CAD_PERGUNTA.CAD_RESPOSTA")
                                        .Include("CAD_PERGUNTA.CAD_PARTICIPACAO")

                               select p).Where(e => e.COD_PESQUISA==id & e.CAD_PERGUNTA.Any(r => r.CAD_PARTICIPACAO.Count() != 0))
                               .AsEnumerable()
                                .Select(a => new Pesquisa()
                                {
                                    Nome = a.NOM_PESQUISA,
                                    Perguntas = a.CAD_PERGUNTA.Select(b => new Pergunta(Comum.TrataPergunta(b))
                                    {
                                        Respostas = b.CAD_RESPOSTA.Select(c => (Comum.TrataResposta(c))).ToList(),
                                        Participacoes = b.CAD_PARTICIPACAO.Select(p => new Participacao()
                                        {
                                            RespostaNula = p.IND_RESPOSTA_NULA,
                                            DataParticipacao = Convert.ToDateTime(p.DTA_PARTICIPACAO).ToString("DD/mm/yyyy HH:MM:ss"),
                                            DataSincronizacao = Convert.ToDateTime(p.DTA_SINCRONIZACAO).ToString("DD/mm/yyyy HH:MM:ss"),
                                            Descricao = p.DSC_RESPOSTA_DISSERTATIVA,
                                            IdResposta = p.COD_RESPOSTA,
                                            IdPergunta = p.COD_PERGUNTA
                                        }).ToList()
                                    }).ToList()
                                }).FirstOrDefault();

                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
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
