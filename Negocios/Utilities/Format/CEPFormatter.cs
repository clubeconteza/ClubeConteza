namespace Negocios.Utilities.Format
{
    public class CEPFormatter : BaseFormatter
    {
        public CEPFormatter()
            : base(DocumentFormats.Cep, "$1-$2", DocumentFormats.CepUnformatted, "$1$2")
        {
        }
    }
}
