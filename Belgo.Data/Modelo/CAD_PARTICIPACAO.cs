//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Belgo.Dados.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class CAD_PARTICIPACAO
    {
        public long COD_PARTICIPACAO { get; set; }
        public Nullable<long> COD_PERGUNTA { get; set; }
        public Nullable<long> COD_RESPOSTA { get; set; }
        public string DSC_RESPOSTA_DISSERTATIVA { get; set; }
        public string IND_RESPOSTA_NULA { get; set; }
        public Nullable<System.DateTime> DTA_PARTICIPACAO { get; set; }
        public Nullable<System.DateTime> DTA_SINCRONIZACAO { get; set; }
    
        public virtual CAD_PERGUNTA CAD_PERGUNTA { get; set; }
        public virtual CAD_RESPOSTA CAD_RESPOSTA { get; set; }
    }
}
