using System;
using System.IO;
using System.Net;
using System.Text;

namespace Negocios.ServicesClient.Vouchers
{
    public class RequisicaoVouchers : IRequisicao
    {
        public string Envia(IServico servico)
        {
            string conteudo;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://vouchers.sodaweb.com.br" + servico.Url());
            request.Method = servico.MetodoRequisicao();
            request.Headers.Add("Authorization", "Token token=\"32dg9jfMhTEO+ToC49NDC85GsarUB83/cOzZ69o3Cph//YAgu0k7OQM+qnubfvZBizeqK5/3En6SBJH1Ns8GTA==\", email=\"ti@clubeconteza.com.br\"");
            request.Accept = "application/json";
            request.ContentType = "application/json";

            string json = servico.CorpoRequisicao();
            byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
            request.GetRequestStream().Write(jsonBytes, 0, jsonBytes.Length);

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
            catch (Exception)
            {
                return null;
            }
        }
    }
}
