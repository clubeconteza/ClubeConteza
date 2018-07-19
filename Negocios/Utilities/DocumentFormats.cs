namespace Negocios.Utilities
{
    public class DocumentFormats
    {
        public static string Cpf = @"(\d{3})[.](\d{3})[.](\d{3})-(\d{2})";
        public static string CpfUnformatted = @"(\d{3})(\d{3})(\d{3})(\d{2})";
        public static string CpfDigitsOnly = @"^\d{11}$";

        public static string Cnpj = @"(\d{2})[.](\d{3})[.](\d{3})\/(\d{4})-(\d{2})";
        public static string CnpjUnformatted = @"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})";
        public static string CnpjDigitsOnly = @"^\d{14}$";

        public static string Cep = @"(\d{5})-(\d{3})";
        public static string CepUnformatted = @"(\d{5})(\d{3})";
        public static string CepDigitsOnly = @"^\d{8}$";
    }
}
