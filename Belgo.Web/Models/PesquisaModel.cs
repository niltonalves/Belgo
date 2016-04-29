using Belgo.Dados.Entidade;
using Belgo.Dados.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Belgo.Web.Models
{
    public class PesquisaModel
    {

        public PesquisaModel()
        {
            Perguntas = new List<PerguntaModel>();
            Participacoes = new List<ParticipacaoModel>();
            
        }
        public long ID { get; set; }
        public string Nome { get; set; }
        public bool Fechado { get; set; }
        public long IdUsuarioCriacao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public List<PerguntaModel> Perguntas { get; set; }
        public List<ParticipacaoModel> Participacoes { get; set; }
        public int TotalPerguntas { get; set; }
        public int TotalParticipacao { get; set; }

        public class PerguntaModel
        {
            public PerguntaModel()
            {
                Respostas = new List<RespostaModel>();
                Participacoes = new List<ParticipacaoModel>();
                Relatorio = new RelatorioModel();
            }
            public Int64 ID { get; set; }
            public Int64 IdPesquisa { get; set; }
            public Int64? IdUsuario { get; set; }
            public string Descricao { get; set; }
            public string Tipo { get; set; }
            public string TipoGrafico { get; set; }
            public short Ordem { get; set; }
            public DateTime? DataCriacao { get; set; }
            public Usuario UsuarioCriacao { get; set; }
            public List<RespostaModel> Respostas { get; set; }
            public List<ParticipacaoModel> Participacoes { get; set; }
            public RelatorioModel Relatorio { get; set; }
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
        public class RelatorioModel
        {

            public long ID { get; set; }
            public string TituloPergunta { get; set; }
            public string DadosSerializados { get; set; }
            public class Grafico
            {
                public string Descricao { get; set; }
                public int Total { get; set; }
            }

        }
        public enum StatusPergunta
        {
            Publicado,
            Editando
        }
    }
}