namespace Negocios.Utilities.Validation
{
    public class CNPJValidator : BaseCadastroPessoaValidator
    {
        public override string RegexFormatted => DocumentFormats.Cnpj;
        public override string RegexUnformatted => DocumentFormats.CnpjUnformatted;
        protected override int DocumentLength => 14;

        public CNPJValidator() : base(false)
        {
        }

        public CNPJValidator(bool isFormatted) : base(isFormatted)
        {
        }

        protected override int[] GetMultiplicadores(int[] digitos)
        {
            if (digitos.Length == DocumentLength - 2)
            {
                return new int[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            }
            else
            {
                return new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            }
        }
    }
}
