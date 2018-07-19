using Boleto.Controller;
using Boleto.Controller.ServicesClient;
using Boleto.Negocios.ServicesClient.Sms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Services;

namespace Boleto
{
    /// <summary>
    /// Summary description for SMS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SMS : WebService
    {
        private readonly string username = "acovir.mkt.web1";
        private readonly string password = "MOInWfdwpd";

        private readonly string numUsu = "gconteza";
        private readonly string senha = "L6z09BGX";

        [WebMethod(Description = "Zenvia - Retorna a lista de novos SMSs recebidos. Uma vez cosultado, o SMS não irá mais ser retornado na chamada deste serviço.")]
        public string ListarNovosSmsRecebidos()
        {
            var servico = new ServicoListarNovosSmsRecebidos();
            servico.Enviador = new RequisicaoSms();
            servico.Envia();

            return servico.Retorno;
        }

        //[WebMethod(Description = "Zenvia - Consulta o status de entrega de uma mensagem previamente enviada usando seu identificador id como referência. Importante: a consulta a um SMS fica disponível por até 72 horas após seu envio.")]
        //public string ConsultaStatusSms(long id)
        //{
        //    string conteudo;
        //    var request = (HttpWebRequest)WebRequest.Create("https://api-rest.zenvia360.com.br/services/get-sms-status/" + id);
        //    request.Method = "GET";
        //    request.Headers.Add("Authorization", Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password))); //Autenticação
        //    request.Accept = "application/json";
        //    request.ContentType = "application/json";

        //    var response = request.GetResponse();
        //    using (Stream responseStream = response.GetResponseStream())
        //    {
        //        StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
        //        conteudo = reader.ReadToEnd();
        //    }

        //    return conteudo;
        //}

        [WebMethod(Description = "Zenvia - Este serviço envia um SMS para o celular do destinatário. Com ele é possível enviar mensagens de texto curtas e longas.")]
        public string EnvioUnicoSms(EnviaSmsController sms)
        {
            var servico = new ServicoEnvioUnicoSms(sms);
            servico.Enviador = new RequisicaoSms();
            servico.Envia();

            return servico.Retorno;
        }

        [WebMethod(Description = "Zenvia - Este serviço envia vários SMS para o celular dos destinatários. Com ele é possível enviar mensagens de texto curtas e longas.")]
        public string EnvioVariosSms(EnviaSmsMultiController sms)
        {
            var servico = new ServicoEnvioVariosSms(sms);
            servico.Enviador = new RequisicaoSms();
            servico.Envia();

            return servico.Retorno;
        }

        [WebMethod(Description = "Zenvia - Retorna a lista de SMSs recebidos em um período definido.")]
        public string ConsultarSmsRecebidosPorPeriodo(DateTime dataInicio, DateTime dataFinal)
        {
            var servico = new ServicoConsultarSmsRecebidosPorPeriodo(dataInicio, dataFinal);
            servico.Enviador = new RequisicaoSms();
            servico.Envia();

            return servico.Retorno;
        }

        //[WebMethod(Description = "Zenvia - Cancela o envio de um SMS previamente agendado. Para realizar o cancelamento, é necessário que tenha sido utilizado o identificador id no momento do envio. Importante: o cancelamento de um SMS só pode ser feito até sua data de agendamento. Após, o SMS terá sido enviado à operadora e não poderá ser cancelado.")]
        //public string CancelamentoSmsAgendado(long id)
        //{
        //    string conteudo;
        //    var request = (HttpWebRequest)WebRequest.Create("https://api-rest.zenvia360.com.br/services/cancel-sms/" + id);
        //    request.Method = "POST";
        //    request.Headers.Add("Authorization", Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password))); //Autenticação
        //    request.Accept = "application/json";
        //    request.ContentType = "application/json";

        //    var response = request.GetResponse();
        //    using (Stream responseStream = response.GetResponseStream())
        //    {
        //        StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
        //        conteudo = reader.ReadToEnd();
        //    }

        //    return conteudo;
        //}

        [WebMethod(Description = "TWW - Envio de mensagens individuais")]
        public string EnviaSMS(SmsController sms)
        {
            var seuNum = string.Format("A{0}B{1}C{2}", sms.TB012_id, sms.TB009_id, sms.TB039_id);
            var celular = string.Concat("55", sms.TB009_Contato.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", ""));

            var ws = new WSReluzCap.ReluzCapWebService();
            var retorno = ws.EnviaSMS(numUsu, senha, seuNum, celular, sms.TB039_Mensagem);

            //Retorna uma string com um dos valores abaixo: + OK – Mensagem aceita para transmissão  + NOK – Mensagem não aceita para transmissão ou pendências financeiras  + Erro  + NA (não disponível) – Sistema não disponível
            return retorno;
        }

        [WebMethod(Description = "TWW - Envio de mensagens em lote")]
        public string EnviaSMSDataSet(List<SmsDataSetController> sms)
        {
            var dt = new DataTable("EnviaSMS");
            dt.Columns.Add("SeuNum");
            dt.Columns.Add("Celular");
            dt.Columns.Add("Mensagem");
            dt.Columns.Add("Agendamento");

            foreach (var item in sms)
            {
                var seuNum = string.Format("A{0}B{1}C{2}", item.TB012_id, item.TB009_id, item.TB039_id);
                var celular = string.Concat("55", item.TB009_Contato.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", ""));
                dt.Rows.Add(seuNum, celular, item.TB039_Mensagem, item.TB039_Agendamento.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            var ds = new DataSet("InDataSet");
            ds.Tables.Add(dt);

            var ws = new WSReluzCap.ReluzCapWebService();
            var retorno = ws.EnviaSMSDataSet(numUsu, senha, ds);

            //Retorna uma string com um dos valores abaixo: + OK – Mensagem aceita para transmissão  + NOK – Mensagem não aceita para transmissão ou pendências financeiras  + Erro  + NA (não disponível) – Sistema não disponível 
            return retorno;
        }

        [WebMethod(Description = "TWW - Verifica os créditos de um Usuário Pré-Pago")]
        public int VerCredito()
        {
            var ws = new WSReluzCap.ReluzCapWebService();
            var retorno = ws.VerCredito(numUsu, senha);

            // Retorna o número de créditos ou -1 se o Usuário não for do tipo Pré-Pago ou -2 em caso de erro nos parâmetros
            return retorno;
        }

        [WebMethod(Description = "TWW - Verifica a validade dos créditos de um Usuário Pré-Pago")]
        public DateTime VerValidade()
        {
            var ws = new WSReluzCap.ReluzCapWebService();
            var retorno = ws.VerValidade(numUsu, senha);

            //Retorna a data de validade dos créditos de um Usuário Pré-Pago. Retorna NOTHING se o Usuário não for do tipo Pré-Pago ou caso haja erro nos parâmetros.
            return retorno;
        }

        [WebMethod(Description = "TWW - Reseta o status de LIDO dos SMS MO desde 1 dia atrás até o momento atual")]
        public string ResetaMOLido()
        {
            var ws = new WSReluzCap.ReluzCapWebService();
            var retorno = ws.ResetaMOLido(numUsu, senha);

            // Retorna OK ou NOK em caso de erro.
            return retorno;
        }

        [WebMethod(Description = "TWW - Mensagens SMS MO não lidas, recebidas nos últimos 4 dias como resposta a SMS enviados anteriormente, e marca esses MOs COMO LIDOS")]
        public DataSet BuscaSMSMONaoLido()
        {
            var ws = new WSReluzCap.ReluzCapWebService();
            var retorno = ws.BuscaSMSMONaoLido(numUsu, senha);

            // Retorna um DataSet chamado OutDataSet contendo uma Tabela chamada SMSMO com no máximo 400 linhas, com as mensagens SMS MO não lidas, recebidas nos últimos 4 dias como resposta a SMS enviados anteriormente, e marca esses MOs COMO LIDOS. Se houverem 400 linhas na tabela, podem haver mais MOs não lidos, e estes devem ser lidos usando chamadas subsequentes à função. Retorna Nothing em caso de erro. 
            return retorno;
        }
    }
}
