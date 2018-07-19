using System.Collections.Generic;

namespace PortalClubeConteza.Models
{
    public class Unidade
    {
        public long Id_T020 { get; set; }
        public double Paginas { get; set; }
        public long Id_T006 { get; set; }
        public long Id_T012 { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CategoriaExibicao { get; set; }
        public int TipoPessoa { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string TextoPortal { get; set; }
        public List<Contato> Contatos { get; set; }
        public Categoria Area { get; set; }
    }
}