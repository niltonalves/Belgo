using Belgo.Dados.Entidade;
using Belgo.Dados.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Belgo.Dados.Util
{
    public class Comum
    {
        public static Usuario TrataUsuario(SCA_USUARIO usuario)
        {
            if (usuario == null)
                return null;

            var retorno = new Usuario()
            {
                Ativo = (usuario.IND_EXCLUIDO == "1" ? true : false),
                Email = usuario.DSC_EMAIL,
                ID = usuario.COD_USUARIO,
                Nome = usuario.NOM_USUARIO,
                Senha = usuario.PSW_SENHA
            };
            return retorno;

        }

        public static Pergunta TrataPergunta(CAD_PERGUNTA pergunta)
        {
            if (pergunta == null)
                return null;

            var retorno = new Pergunta()
            {
                ID = pergunta.COD_PERGUNTA,
                DataCriacao = pergunta.DTA_CRIACAO,
                Descricao = pergunta.DSC_PERGUNTA,
                Ordem = Convert.ToInt16(pergunta.NUM_ORDEM_PERGUNTA),
                Tipo = pergunta.IND_TPO_PERGUNTA,
                IdPesquisa = pergunta.COD_PESQUISA,
                IdUsuario = pergunta.COD_USER_CRIACAO,
                UsuarioCriacao = TrataUsuario(pergunta.SCA_USUARIO)
            };
            return retorno;
        }
        public static Resposta TrataResposta(CAD_RESPOSTA resposta)
        {
            if (resposta == null)
                return null;

            var retorno = new Resposta()
            {
                ID = resposta.COD_RESPOSTA,
                DataCriacao = resposta.DTA_CRIACAO,
                Descricao = resposta.DSC_RESPOSTA,
                IdPergunta = resposta.COD_PERGUNTA,
                IdPesquisa = resposta.CAD_PERGUNTA.COD_PESQUISA,
                UsuarioCriacao = TrataUsuario(resposta.SCA_USUARIO)
            };
            return retorno;
        }

    }
}
