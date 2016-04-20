using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Belgo.Web.Models
{
    public class ParticipacaoModel
    {
        public long ID { get; set; }
        public long? IdPergunta { get; set; }
        public long? IdResposta { get; set; }
        public string Token { get; set; }
        public string Descricao { get; set; }
        public string RespostaNula { get; set; }
        public string DataParticipacao { get; set; }
        public string DataSincronizacao { get; set; }

    }
}
