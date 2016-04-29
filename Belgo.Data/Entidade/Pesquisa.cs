using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Belgo.Dados.Entidade
{
    public class Pesquisa
    {
        public Pesquisa()
        {
            Perguntas = new List<Pergunta>();
        }
        public long ID { get; set; }
        public string Nome { get; set; }
        public bool Fechado { get; set; }
        public long IdUsuarioCriacao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public List<Pergunta> Perguntas { get; set; }
        public Usuario UsuarioCriacao { get; set; }
        public int TotalParticipacao { get; set; }
    }
}
