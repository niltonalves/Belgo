using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Belgo.Dados.Entidade
{
    public class Usuario
    {
        public long ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
    }
}
