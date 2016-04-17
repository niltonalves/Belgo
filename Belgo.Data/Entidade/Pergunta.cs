using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Belgo.Dados.Entidade
{
    public class Pergunta
    {
        public Pergunta() { }
        public Pergunta(Pergunta _pergunta)
        {
            this.ID = _pergunta.ID;
            this.Descricao = _pergunta.Descricao;
            this.Tipo = _pergunta.Tipo;
            this.Ordem = _pergunta.Ordem;
            this.DataCriacao = _pergunta.DataCriacao;
            this.IdPesquisa = _pergunta.IdPesquisa;
        }

        public Int64 ID { get; set; }
        public Int64 IdPesquisa{ get; set; }
        public Int64? IdUsuario { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public short Ordem { get; set; }
        public DateTime? DataCriacao { get; set; }
        public Usuario UsuarioCriacao { get; set; }
        public List<Resposta> Respostas { get; set; }
        public List<Participacao> Participacoes { get; set; }
    }
}
