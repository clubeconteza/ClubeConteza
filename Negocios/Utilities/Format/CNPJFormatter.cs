namespace Negocios.Utilities.Format
{
    public class CNPJFormatter : BaseFormatter
    {
        public CNPJFormatter() 
            : base(DocumentFormats.Cnpj, "$1.$2.$3/$4-$5", DocumentFormats.CnpjUnformatted, "$1$2$3$4$5")
        {
        }
    }
}
