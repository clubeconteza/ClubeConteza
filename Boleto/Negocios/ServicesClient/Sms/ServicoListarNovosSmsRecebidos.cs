namespace Boleto.Negocios.ServicesClient.Sms
{
    public class ServicoListarNovosSmsRecebidos : IServico
    {
        public IRequisicao Enviador { get; set; }

        public string Retorno { get; private set; }

        public ServicoListarNovosSmsRecebidos()
        {
        }

        public void Envia()
        {
            Retorno = Enviador.Envia(this);
        }

        public string Url()
        {
            return "/received/list";
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