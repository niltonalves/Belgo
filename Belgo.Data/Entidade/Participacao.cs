using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Belgo.Dados.Entidade
{
    public class Participacao
    {
        public long ID { get; set; }
        public long? IdPergunta { get; set; }
        public int IdResposta { get; set; }
        public string Descricao { get; set; }
        public string RespostaNula { get; set; }
        public DateTime? DataParticipacao { get; set; }
        public DateTime? DataSincronizacao { get; set; }
    }
}
