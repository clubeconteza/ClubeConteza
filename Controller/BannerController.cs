using System;

namespace Controller
{
    public class BannerController
    {

        public long TB019_Id { get; set; }
        //TB006_id
        //TB012_id
        public int TB019_Sessao { get; set; }
        public string TB019_URLImagem { get; set; }
        public string TB019_NomeArquivo { get; set; }
        public string TB019_Titulo { get; set; }
        public string TB019_Link { get; set; }
        public int TB019_LinkFormaAberturaLink { get; set; }
        public string TB019_ToolTip { get; set; }
        public DateTime TB019_DataInicio { get; set; }
        public DateTime TB019_DataFim { get; set; }

        public DateTime TB019_CadastradoEm { get; set; }
        public Int64 TB019_CadastradoPor { get; set; }
        public DateTime TB019_AlteradoEm { get; set; }
        public Int64 TB019_AlteradoPor { get; set; }


        public string TTB019_StatusS { get; set; }
        public enum TTB019_StatusE
        {
            Cadastrado = 1,
            Agendado = 2,
            Ativo = 3,
            Cancelado = 4,
            Arquivo = 5
        }

    }
}
