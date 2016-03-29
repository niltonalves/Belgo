using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Belgo.Web.Models
{
    public class PesquisaModel
    {
        //Construtor
        public PesquisaModel()
        {
            Perguntas = new List<PerguntaModel>();
            Alternativas = new List<AlternativaModel>();
        }

        public int ID { get; set; }
        public string Nome { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataPublicacao { get; set; }
        public StatusPergunta Status { get; set; }
        public List<PerguntaModel> Perguntas { get; set; }
        public List<AlternativaModel> Alternativas { get; set; }

        public class PerguntaModel
        {
            public int ID { get; set; }
            public string Titulo { get; set; }
            public DateTime DataCadastro { get; set; }
            public bool Ativo { get; set; }
        }

        public class AlternativaModel
        {
            public int ID { get; set; }
            public string Titulo { get; set; }
            public TipoResposta Tipo { get; set; }

            public enum TipoResposta
            {
                MultiplaEscolha = 1,
                Dissertativa,
                Outras
            }
        }
        public enum StatusPergunta
        {
            Publicado,
            Editando
        }
    }
}