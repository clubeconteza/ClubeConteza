namespace Negocios.ServicesClient.Vouchers
{
    public class ServicoListaCuponsVouchersValidados : IServico
    {
        private int mes;
        private int ano;

        public IRequisicao Enviador { get; set; }

        public string Retorno { get; private set; }

        public ServicoListaCuponsVouchersValidados(int mes, int ano)
        {
            this.mes = mes;
            this.ano = ano;
        }

        public void Envia()
        {
            Retorno = Enviador.Envia(this);
        }

        public string Url()
        {
            return "/api/v1/users/used_coupons_vouchers";
        }

        public string MetodoRequisicao()
        {
            return "POST";
        }

        public string CorpoRequisicao()
        {
            return "{\"month\":\"" + mes.ToString("00") + "\",\"year\":\"" + ano.ToString("0000") + "\"}";
        }
    }
}
