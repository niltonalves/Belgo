using Belgo.Dados.Entidade;
using Belgo.Web.Models;
using Belgo.Web.Util;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static Belgo.Web.Models.PesquisaModel;
using static Belgo.Web.Util.Enumerador;
using Newtonsoft.Json;
using System.Collections;

namespace Belgo.Web.Controllers
{
    public class RelatorioController : BaseController
    {
        public object ChartsData { get; private set; }

        public ActionResult Index()
        {
            try
            {
                var api = new RestApi();
                api.Method = Method.GET;
                api.Resource = RestApi.Resources.Pesquisa;
                api.Action = "fechado";
                api.AdicionarParametro(new RestSharp.Parameter() { Type = ParameterType.UrlSegment, Name = "fechado", Value = "sim" });
                var lista = api.Executar<List<PesquisaModel>>();

                return View(lista.Data);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public ActionResult Grafico(int id)
        {
            try
            {
                var api = new RestApi();
                api.Method = Method.GET;
                api.Resource = RestApi.Resources.Pesquisa;
                api.Action = "relatorioparticipacao";
                api.AdicionarParametro(new RestSharp.Parameter() { Type = ParameterType.UrlSegment, Name = "id", Value = id });
                var lista = api.Executar<PesquisaModel>();

                //não há dado
                if (lista.Data == null)
                {
                    MostrarAlerta(TipoAlerta.Erro, "Não há dados para geração do gráfico.");
                    return RedirectToAction("Index");
                }
                

                return View(lista.Data);
            }
            catch (Exception ex)
            {

                throw ex;

            }

        }
        public ActionResult ExportarExcel(long id)
        {
            try
            {
                var api = new RestApi();
                api.Method = Method.GET;
                api.Resource = RestApi.Resources.Pesquisa;
                api.Action = "relatorioparticipacao";
                api.AdicionarParametro(new RestSharp.Parameter() { Type = ParameterType.UrlSegment, Name = "id", Value = id });
                var lista = api.Executar<PesquisaModel>();

                if (lista.Data == null)
                {
                    MostrarAlerta(TipoAlerta.Erro, "Não há dados para exportação");
                    return RedirectToAction("Index");
                }

                var pesquisa = lista.Data;

                var tiposPergunta = from Enumerador.TipoPergunta s in Enum.GetValues(typeof(Enumerador.TipoPergunta))
                                    select new { ID = s, Name = Enumerador.GetDescription(s) };


                //total de participacao

                var total = pesquisa.TotalParticipacao;
                StringBuilder sb = new StringBuilder();
                sb.Append("<table width='800' cellspacing='0' cellpadding='2'>");
                sb.Append("<tr><td align='center' style='background-color: #e31d1a;color:#ffffff' colspan = '3' height='50px'><font face='4'><b>" + pesquisa.Nome + "</b></font></td></tr>");
                sb.Append("<tr><td colspan = '3'></td></tr>");
                sb.Append("<tr><td><b>Total de participações: </b> " + total + "</td><td></td><td><b>Data: </b>" + DateTime.Now + " </td></tr>");
                sb.Append("</table>");

                sb.Append("<table border = '1' width='800'>");
                foreach (var pergunta in pesquisa.Perguntas)
                {
                    var descricaoTipo = tiposPergunta.FirstOrDefault(p => p.ID.ToString() == pergunta.Tipo).Name;
                    if (pergunta.Tipo == "D")
                    {
                        sb.Append("<tr height='50'>");
                        sb.Append(string.Format("<td style='background-color: #dfdfdf;width:5.5px' ><b>{0}</b></td>", pergunta.Descricao));
                        sb.Append(string.Format("<td style='background-color: #dfdfdf;width:5.5px' ><b></b></td>"));
                        sb.Append(string.Format("<td style='background-color: #dfdfdf'><b>{0}</b></td>", descricaoTipo));
                        sb.Append("</tr>");
                        foreach (var particpacao in pergunta.Participacoes.Where(c => !String.IsNullOrEmpty(c.Descricao)))
                        {
                            sb.Append("<tr>");
                            sb.Append(string.Format("<td colspan='3'>{0}</td>", particpacao.Descricao));
                            sb.Append("</tr>");
                        }
                    }
                    else
                    {
                        sb.Append("<tr height='50'>");
                        sb.Append(string.Format("<td style='background-color: #dfdfdf' ><b>{0}</b></td>", pergunta.Descricao));
                        sb.Append(string.Format("<td style='background-color: #dfdfdf' ><b>Quantidade Resposta</b></td>", pergunta.Descricao));
                        sb.Append(string.Format("<td style='background-color: #dfdfdf' ><b>{0}</b></td>", descricaoTipo));
                        sb.Append("</tr>");
                        foreach (var resposta in pergunta.Respostas)
                        {
                            sb.Append("<tr>");
                            sb.Append(string.Format("<td width='500'>{0}</td>", resposta.Descricao));
                            sb.Append(string.Format("<td>{0}</td>", pergunta.Participacoes.Count(c => c.IdResposta == resposta.ID)));
                            sb.Append("</tr>");
                        }

                    }

                }
                sb.Append("</table>");
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + string.Format("Relatorio-Participacao-{0}.xls", pesquisa.Nome));
                Response.ContentType = "application/vnd.ms-excel;charset=utf-8";

                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.Charset = "65001";
                byte[] b = new byte[] { 0xef, 0xbb, 0xbf };
                Response.BinaryWrite(b);

                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sb.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {

                throw ex;

            }
            return View();

        }
    }
}
