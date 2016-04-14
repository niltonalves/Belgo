using Belgo.Dados.Entidade;
using Belgo.Dados.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Belgo.Web.Models
{
    public class PesquisaModel
    {

        public PesquisaModel()
        {
            Perguntas = new List<PerguntaModel>();
        }
        public long ID { get; set; }
        public string Nome { get; set; }
        public bool Fechado { get; set; }
        public long IdUsuarioCriacao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public List<PerguntaModel> Perguntas { get; set; }
        public int TotalPerguntas { get; set; }
        //        public Usuario UsuarioCriacao { get; set; }

        public class PerguntaModel
        {
            public PerguntaModel()
            {
                this.Respostas = new List<RespostaModel>();
            }
            public Int64 ID { get; set; }
            public Int64 IdPesquisa { get; set; }
            public Int64? IdUsuario { get; set; }
            public string Descricao { get; set; }
            public string Tipo { get; set; }
            public short Ordem { get; set; }
            public DateTime? DataCriacao { get; set; }
            public Usuario UsuarioCriacao { get; set; }
            public List<RespostaModel> Respostas { get; set; }
        }

        public class RespostaModel
        {
            public long ID { get; set; }
            public long IdPergunta { get; set; }
            public long IdPesquisa { get; set; }
            public string Descricao { get; set; }
            public DateTime? DataCriacao { get; set; }
            public Usuario UsuarioCriacao { get; set; }

        }



        ////Construtor
        //public PesquisaModel()
        //{
        //    Perguntas = new List<PerguntaModel>();
        //    Alternativas = new List<AlternativaModel>();
        //}

        //public int ID { get; set; }
        //public string Nome { get; set; }
        //public DateTime DataCadastro { get; set; }
        //public DateTime DataPublicacao { get; set; }
        //public StatusPergunta Status { get; set; }
        //public List<PerguntaModel> Perguntas { get; set; }
        //public List<AlternativaModel> Alternativas { get; set; }

        //public class PerguntaModel
        //{
        //    public int ID { get; set; }
        //    public string Titulo { get; set; }
        //    public DateTime DataCadastro { get; set; }
        //    public bool Ativo { get; set; }
        //}

        //public class AlternativaModel
        //{
        //    public int ID { get; set; }
        //    public string Titulo { get; set; }
        //    public TipoResposta Tipo { get; set; }

        //    public enum TipoResposta
        //    {
        //        MultiplaEscolha = 1,
        //        Dissertativa,
        //        Outras
        //    }
        //}
        public enum StatusPergunta
        {
            Publicado,
            Editando
        }
    }
}