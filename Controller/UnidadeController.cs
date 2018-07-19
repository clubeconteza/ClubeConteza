using System;
using System.Collections.Generic;

namespace Controller
{
    public class UnidadeController
    {
        public long TB020_id { get; set; }
        //public Int64    TB021_id                { get; set; }
        public int TB020_Matriz { get; set; }
        public long TB012_id { get; set; }

        public long TB012_idCorporativo { get; set; }


        public string TB020_RazaoSocial { get; set; }
        public string TB020_NomeFantasia { get; set; }
        public string TB020_NomeExibicaoDetalhes { get; set; }
        public string TB020_CategoriaExibicao { get; set; }
        public int TB020_TipoPessoa { get; set; }
        public string TB020_Documento { get; set; }
        public long TB006_id { get; set; }
        public string TB020_Cep { get; set; }
        public string TB020_Logradouro { get; set; }
        public string TB020_Numero { get; set; }
        public string TB020_Bairro { get; set; }
        public string TB020_Complemento { get; set; }
        public string TB020_TextoPortal { get; set; }
        public byte[] TB020_logo { get; set; }
        public DateTime TB020_CadastradoEm { get; set; }
        public long TB020_CadastradoPor { get; set; }
        public DateTime TB020_AlteradoEm { get; set; }
        public long TB020_AlteradoPor { get; set; }
        public string TB020_AlteradoPorNome { get; set; }
        public byte[] TB020_Desconto { get; set; }
        public double Paginas { get; set; }

        public string TB020_StatusS { get; set; }
        public enum TB020_StatusE
        {
            Cadastrado = 0,
            Ativo = 1,
            Inativo = 2 //Não Alerar esta codigo.       
        }

        public PessoaController Pessoa { get; set; }
        public PaisController Pais { get; set; }
        public EstadoController Estado { get; set; }

        
        public MunicipioController Municipio { get; set; }
        public CategoriaController Nivel1 { get; set; }
        public List<CategoriaController> Neveis { get; set; }
        public List<ContatoController> Contatos { get; set; }

        public List<wContatoController> wContatos { get; set; }
        public ContratosController Contrato { get; set; }
        public List<ContratosController> ContratosVinculados { get; set; }
        public List<UnidadeController> UnidadesVinculadas { get; set; }

        public wCategoriaController Area { get; set; }

        public List<wCategoriaController> Areas { get; set; }

        public CategoriaController Categoria { get; set; }


    }
}
