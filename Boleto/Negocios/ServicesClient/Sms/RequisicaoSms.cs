using System;
using System.IO;
using System.Net;
using System.Text;

namespace Boleto.Negocios.ServicesClient.Sms
{
    public class RequisicaoSms : IRequisicao
    {
        public string Envia(IServico servico)
        {
            var username = "acovir.mkt.web1";
            var password = "MOInWfdwpd";

            string conteudo;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api-rest.zenvia360.com.br/services" + servico.Url());
            request.Method = servico.MetodoRequisicao();
            request.Headers.Add("Authorization", Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password)));
            request.Accept = "application/json";
            request.ContentType = "application/json";

            if (!string.IsNullOrEmpty(servico.CorpoRequisicao()))
            {
                string json = servico.CorpoRequisicao();
                byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
                request.GetRequestStream().Write(jsonBytes, 0, jsonBytes.Length);
            }

            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    conteudo = reader.ReadToEnd();
                }

                return conteudo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}