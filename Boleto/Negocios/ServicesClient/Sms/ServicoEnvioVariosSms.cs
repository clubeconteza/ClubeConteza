using Boleto.Controller.ServicesClient;
using Newtonsoft.Json;

namespace Boleto.Negocios.ServicesClient.Sms
{
    public class ServicoEnvioVariosSms : IServico
    {
        private EnviaSmsMultiController sms;

        public IRequisicao Enviador { get; set; }

        public string Retorno { get; private set; }

        public ServicoEnvioVariosSms(EnviaSmsMultiController sms)
        {
            this.sms = sms;
        }

        public void Envia()
        {
            Retorno = Enviador.Envia(this);
        }

        public string Url()
        {
            return "/send-sms-multiple";
        }

        public string MetodoRequisicao()
        {
            return "POST";
        }

        public string CorpoRequisicao()
        {
            return JsonConvert.SerializeObject(sms);
        }
    }
}