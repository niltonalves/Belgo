using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Configuration;
using Belgo.Web.Models;

namespace Belgo.Web.Util
{
    public class RestApi
    {
        string urlApi = ConfigurationManager.AppSettings["urlApi"].ToString();
        List<Parameter> parametros = new List<Parameter>();

        /// <summary>
        /// Executa a chamada a api
        /// </summary>
        /// <typeparam name="T">Objeto de retorno</typeparam>
        /// <param name="resource">Tipo do resource</param>
        /// <param name="method">Método de uso</param>
        /// <returns>Objeto deserializado da pi</returns>
        public IRestResponse Executar<T>(Resource resource, Method method) where T : new()
        {
            try
            {
                var cliente = new RestClient(urlApi);
                var request = new RestRequest();
                request.Resource = resource.ToString();
                request.Method = method;
                request.RequestFormat = DataFormat.Json;
                request.Parameters.Clear();
                parametros.ForEach(delegate (Parameter param) {

                    request.Parameters.Add(param);
                });
                var response = cliente.Execute<T>(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception(response.StatusDescription);

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /// <summary>
        /// Adiciona paramêtro para executação da api
        /// </summary>
        /// <param name="parametro">Entidade de paramêtro</param>
        public void AdicionarParametro(Parameter parametro)
        {
            parametros = parametros ?? new List<Parameter>();
            parametros.Add(parametro);
        }
        /// <summary>
        /// Resource disponíveis
        /// </summary>
        public enum Resource
        {
            Pesquisa = 1,
            Pergunta,
            Resposta,
            Participacao

        }
    }

}
