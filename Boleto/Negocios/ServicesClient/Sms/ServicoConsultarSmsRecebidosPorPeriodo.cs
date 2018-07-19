using System;

namespace Boleto.Negocios.ServicesClient.Sms
{
    public class ServicoConsultarSmsRecebidosPorPeriodo : IServico
    {
        private DateTime dataInicio;
        private DateTime dataFinal;

        public IRequisicao Enviador { get; set; }

        public string Retorno { get; private set; }

        public ServicoConsultarSmsRecebidosPorPeriodo(DateTime dataInicio, DateTime dataFinal)
        {
            this.dataInicio = dataInicio;
            this.dataFinal = dataFinal;
        }

        public void Envia()
        {
            Retorno = Enviador.Envia(this);
        }

        public string Url()
        {
            return "/received/search/" + dataInicio.ToString("s") + "/" + dataFinal.ToString("s");
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