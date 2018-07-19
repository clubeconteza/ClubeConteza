using System.Collections.Generic;

namespace PortalClubeConteza.Models
{
    public class UnidadeDetalhe
    {
        public long Id_T020 { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string CategoriaExibicao { get; set; }
        public int TipoPessoa { get; set; }
        public int Matriz { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string TextoPortal { get; set; }
        public Municipio Municipio { get; set; }
        public Estado Estado { get; set; }
        public Pais Pais { get; set; }
        public List<Categoria> Areas { get; set; }
        public List<Contato> Contatos { get; set; }
    }
}