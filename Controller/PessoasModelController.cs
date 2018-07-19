using System;

namespace Controller
{
    public class PessoasModelController
    {
        public long Id { get; set; }
        public string CpfCnpj { get; set; }
        public string NomeCompleto { get; set; }
        public string NomeExibicao { get; set; }
        public int? Sexo { get; set; }
        public string Rg { get; set; }
        public string RgOrgaoEmissor { get; set; }
        public DateTime? DataNascimento { get; set; }
        public long? Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public long? IdMunicipio { get; set; }
        public string MaeNome { get; set; }
        public DateTime? MaeDataNascimento { get; set; }
        public string PaiNome { get; set; }
        public DateTime? PaiDataNascimento { get; set; }
    }
}
