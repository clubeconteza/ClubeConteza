namespace Controller
{
    public class UnidadesModelController
    {
        public long Id { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public int? TipoPessoa { get; set; }
        public string Documento { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public long? IdMunicipio { get; set; }
    }
}
