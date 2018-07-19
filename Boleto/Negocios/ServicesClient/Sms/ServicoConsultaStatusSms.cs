namespace Boleto.Negocios.ServicesClient.Sms
{
    public class ServicoConsultaStatusSms : IServico
    {
        private long id;

        public IRequisicao Enviador { get; set; }

        public string Retorno { get; private set; }

        public ServicoConsultaStatusSms(long id)
        {
            this.id = id;
        }

        public void Envia()
        {
            Retorno = Enviador.Envia(this);
        }

        public string Url()
        {
            return "/get-sms-status/" + id;
        }

        public string MetodoRequisicao()
        {
            return "GET";
        }

        public string CorpoRequisicao()
        {
            return string.Empty;
        }
    }
}