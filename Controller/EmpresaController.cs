using System;

namespace Controller
{
    public class EmpresaController
    {
        public Int64 TB001_id { get; set; }
        public int TB001_EMatriz { get; set; }
        public string TB001_Matriz { get; set; }
        public string TB001_RazaoSocial { get; set; }
        public string TB001_NomeFantasia { get; set; }
        public string TB001_UF { get; set; }
        public string TB001_CNPJ { get; set; }
        public string TB001_Cep { get; set; }
        public string TB001_Logradouro { get; set; }
        public string TB001_Numero { get; set; }
        public string TB001_Complemento { get; set; }
        public byte[] TB001_Logo { get; set; }

        public string Cidade { get; set; }
        public string UF { get; set; }

        //public Int64 TB001_Municipio_ID { get; set; }
        //TB001_CadastradoEm { get; set; }
        //TB001_CadastradoPor { get; set; }
        //TB001_AlteradoEm { get; set; }
        //TB001_AlteradoPor { get; set; }

    }
}
