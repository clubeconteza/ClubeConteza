namespace Negocios.Utilities.Format
{
    public class CPFFormatter : BaseFormatter
    {
        public CPFFormatter()
            : base(DocumentFormats.Cpf, "$1.$2.$3-$4", DocumentFormats.CpfUnformatted, "$1$2$3$4")
        {
        }
    }
}
