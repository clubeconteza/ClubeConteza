
using System;


namespace Controller
{
    public class mensalidadePremiadaController
    {

        public long         TB042_id                { get; set; }
        public long         TB042_PontosMinimo      { get; set; }
        public long         TB042_PontosMaximo      { get; set; }
        public DateTime     TB042_DataSorteio       { get; set; }
        public string       TB042_Descricao         { get; set; }
        public double       TB042_VlrUni            { get; set; }
        public int          TB042_Quantidade        { get; set; }
        public double       TB042_VlrTotal          { get; set; }
        public string       TB042_Concurso          { get; set; }
        public string       TB042_Bilhete1          { get; set; }
        public string       TB042_Bilhete2          { get; set; }
        public string       TB042_Bilhete3          { get; set; }
        public string       TB042_Bilhete4          { get; set; }
        public string       TB042_Bilhete5          { get; set; }
        public string       TB042_NumeroDaSorte     { get; set; }
        public DateTime     TB042_CadastradoEm      { get; set; }
        public long         TB042_CadastradoPor     { get; set; }
        public DateTime     TB042_AlteradoEm        { get; set; }
        public long         TB042_AlteradoPor       { get; set; }
        public string       TB042_AlteradoPorNome   { get; set; }
        public string       TB042_StatusS           { get; set; }
        public enum         TB042_StatusE
        {
            Cadastrato  = 1,
            Sorteado    = 2,
            Contemplado = 3,
            Cancelado   = 4
        }


        public string       Email               {  get; set; }
        public string       Senha               { get; set; }
        public string       Token               { get; set; }
        public string       url                 { get; set; }

        public long                 TB012_id    { get; set; }
        public PessoaController     titular     { get; set; }
        public ContatoController    contato     { get; set; }
        public ContratosController  contrato    { get; set; }

    }
}