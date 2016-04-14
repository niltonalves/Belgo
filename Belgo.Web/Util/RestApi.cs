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

        public Method Method { get; set; }
        public Resources Resource { get; set; }

        /// <summary>
        /// Executa a chamada a api
        /// </summary>
        /// <typeparam name="T">Objeto de retorno</typeparam>
        /// <param name="resource">Tipo do resource</param>
        /// <param name="method">Método de uso</param>
        /// <returns>Objeto deserializado da pi</returns>
        public IRestResponse<T> Executar<T>() where T : new()
        {
            try
            {
                RestRequest request;
                var cliente = new RestClient(urlApi);
                var segment = parametros.FirstOrDefault(s => s.Type == ParameterType.UrlSegment);

                if (segment == null)
                    request = new RestRequest(Resource.ToString());
                else
                    request = new RestRequest(string.Format("{0}/{1}", Resource.ToString(), segment.Value));
                 
                request.Method = this.Method;
                //request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.Parameters.Clear();
                parametros.Where(p => p.Type != ParameterType.UrlSegment).ToList()
                    .ForEach(p =>
                {
                    request.AddParameter(p.Name, p.Value);

                });

                var response = cliente.Execute<T>(request);
                //if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode== System.Net.HttpStatusCode.NotFound)
                //    throw new Exception(response.StatusDescription);

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
        public enum Resources
        {
            Pesquisa = 1,
            Pergunta,
            Resposta,
            Participacao

        }
    }

}
