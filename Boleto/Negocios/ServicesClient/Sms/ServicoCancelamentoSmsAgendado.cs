namespace Boleto.Negocios.ServicesClient.Sms
{
    public class ServicoCancelamentoSmsAgendado : IServico
    {
        private long id;

        public IRequisicao Enviador { get; set; }

        public string Retorno { get; private set; }

        public ServicoCancelamentoSmsAgendado(long id)
        {
            this.id = id;
        }

        public void Envia()
        {
            Retorno = Enviador.Envia(this);
        }

        public string Url()
        {
            return "/cancel-sms/" + id;
        }

        public string MetodoRequisicao()
        {
            return "POST";
        }

        public string CorpoRequisicao()
        {
            return string.Empty;
        }
    }
}