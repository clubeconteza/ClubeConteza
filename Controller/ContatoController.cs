using System;

namespace Controller
{
    public class ContatoController
    {

        public long     TB009_id                    { get; set; }
        public string   TB009_Contato               { get; set; }
        public int      TB009_ExibirPortal          { get; set; }
        public int      TB012_ProximoCodDependente { get; set; }

        
        public string   TB009_Nota                  { get; set; }
        public string   TB009_TipoS                 { get; set; }
        public DateTime TB009_CadastradoEm          { get; set; }
        public Int64    TB009_CadastradoPor         { get; set; }
        public string   TB009_CadastradoPorNome     { get; set; }
        public DateTime TB009_AlteradoEm            { get; set; }
        public Int64    TB009_AlteradoPor           { get; set; }
        public string   TB009_AlteradoPorNome       { get; set; }
        public string   TB004_Cep                   { get; set; }
        public string   TB009_Logradouro            { get; set; }
        public string   TB009_Numero                { get; set; }
        public string   TB009_Bairro                { get; set; }
        public string   TB009_Complemento           { get; set; }

        public enum TB009_TipoE
        {
            Celular     = 1,
            Fixo        = 2,
            Email       = 3,
            Nextel      = 4
        }

        public PessoaController Pessoa { get; set; }

        public MunicipioController Municipio { get; set; }

        public long     TB020_id                    { get; set; } //Chave Unidade
        public long     TB012_id                    { get; set; } //Chave 
        public long TB013_id { get; set; } //Chave 

    }
}
