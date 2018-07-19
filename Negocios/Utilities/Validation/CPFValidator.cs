namespace Negocios.Utilities.Validation
{
    public class CPFValidator : BaseCadastroPessoaValidator
    {
        public override string RegexFormatted => DocumentFormats.Cpf;
        public override string RegexUnformatted => DocumentFormats.CpfUnformatted;
        protected override int DocumentLength => 11;

        public CPFValidator() : base(false)
        {
        }

        public CPFValidator(bool isFormatted) : base(isFormatted)
        {
        }

        protected override int[] GetMultiplicadores(int[] digitos)
        {
            if (digitos.Length == DocumentLength - 2)
            {
                return new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            }
            else
            {
                return new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            }
        }
    }
}
