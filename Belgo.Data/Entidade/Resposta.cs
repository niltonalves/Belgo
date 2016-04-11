using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Belgo.Dados.Entidade
{
    public class Resposta
    {
        public long ID { get; set; }
        public long IdPergunta { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public Usuario UsuarioCriacao { get; set; }

    }
}
