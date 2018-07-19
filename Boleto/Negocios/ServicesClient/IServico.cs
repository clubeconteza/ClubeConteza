namespace Boleto.Negocios.ServicesClient
{
    public interface IServico
    {
        IRequisicao Enviador { get; set; }

        string Retorno { get; }

        void Envia();

        string Url();

        string MetodoRequisicao();

        string CorpoRequisicao();
    }
}