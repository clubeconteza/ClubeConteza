using System;
using System.Collections.Generic;

namespace Controller
{
    public class ContratosController
    {
        public long         TB012_Id                            { get; set; }
        public long         TB012_NParcelas                     { get; set; }
        public long         TB012_Corporativo                   { get; set; }
        public long         TB012_Pai                           { get; set; }
        public long         TB012_Filho                         { get; set; }
        public long         TB012_CodCartao                     { get; set; }
        public int          TB012_DiaVencimento                 { get; set; }
        public int          TB012_Edicao                        { get; set; }  
        public int          TB012_TipoContrato                  { get; set; }
        // Tipos de Contrato
        //1 - Familiar
        //2 - Parceiro
        //3 - Corporativo
        //4 - Familiar Corporativo
        //5 - Familiar Parceiro
        //public string   TB012_InformacoesPortal     { get; set; }
        public string       TB012_TipoContratoS                 { get; set; }
        public enum         TB012_TipoContratoE
        {
            Familiar = 1,
            Parceiro = 2,
            Corporativo = 3,
            Familiar_Corporativo = 4,
            Familiar_Parceiro = 5
        }
        public DateTime     TB012_Inicio                        { get; set; }
        public DateTime     TB012_Fim                           { get; set; }
        public DateTime     TB012_CadastradoEm                  { get; set; }
        public Int64        TB012_CadastradorPor                { get; set; }
        public DateTime     TB012_AlteradoEm                    { get; set; }
        public int          TB012_AceiteContrato                { get; set; }
        public DateTime     TB012_DataAceiteContrato            { get; set; }
        public Int64        TB012_AlteradoPor                   { get; set; }
        public string       TB004_Cep                           { get; set; }
        public Int16        TB012_VSContrato                    { get; set; }
        public Int64        TB006_id                            { get; set; }
        public string       TB012_Logradouro                    { get; set; }
        public string       TB012_Numero                        { get; set; }
        public string       TB012_Bairro                        { get; set; }
        public string       TB012_Complemento                   { get; set; }
        public string       TB012_CicloContrato                 { get; set; }
        public double       TB016_ParcelaCancelamentoValor      { get; set; }
        public string       TB012_StatusS                       { get; set; }
        public long         TB012_TotalVoucher                  { get; set; }
        public long         TB012_TotalCupons                   { get; set; }
        public long         Pontos                              { get; set; }
        public enum TB012_StatusE
        {
            Cadastrado      = 0,
            Ativo           = 1,
            Bloqueado       = 2,
            Inativo         = 3,
            Inadimplente    = 4,
            Cancelado       = 5,
            Negociado       = 6
        }
        public string       TB020_RazaoSocial                   { get; set; }
        public string       TB020_NomeFantasia                  { get; set; }
        public string       TB012_ContratoCancelarMotivoS       { get; set; }
        public string       TB012_ContratoCancelarData          { get; set; }
        public string       TB020_Documento                     { get; set; }
        public enum TB012_ContratoCancelarMotivoE
        {
            Falecimento = 1,
            Mudou = 2,
            Outros = 3,
            MigrouTitular = 4,
            NãoUtilizaPlano = 5

        }
        /*Documentos do contrato*/
        public Int64        TB026_id                            { get; set; }
        public DateTime     TB026_DataRelatorio                 { get; set; }
        public string       TB026_Relatorio                     { get; set; }
        public DateTime     TB026_CadastradoEm                  { get; set; }
        public Int64        TB026_CadastradoPor                 { get; set; }
        public string       TB026_TipoS                         { get; set; }
        public enum         TB026_TipoE
        {
            Alteração = 1,
            Cancelamento = 2
        }
        public Int64        TB013_id                            { get; set; }
        public string       TB013_CPFCNPJ                       { get; set; }
        public string       TB013_NomeCompleto                  { get; set; }
        public string       TB011_NomeExibicao                  { get; set; }        
        public Int64        TB002_id                            { get; set; }
        public string       TB002_Ponto                         { get; set; }
        public string       TB015_Plano                         { get; set; }
        public double       TB016_Valor                         { get; set; }
        public DateTime     TB016_Vencimento                    { get; set; }
        public long         TB012_NumeroDaSorte                 { get; set; }
        public string       TB012_ContratoCancelarDescricao     { get; set; }
        public PessoaController         Titular                     { get; set; }
        public PessoaController         Dependente                  { get; set; }
        public List<PessoaController>   Dependentes                 { get; set; }
        public PontoDeVendaController   PontoDeVenda                { get; set; }
        public PaisController           Pais                        { get; set; }
        public EstadoController         Estado                      { get; set; }
        public MunicipioController      Municipio                   { get; set; }
        public UnidadeController        Unidade                     { get; set; }
        public PlanoController          Plano                       { get; set; }
        public PlanoController          PlanoAnterior               { get; set; }
        public UsuarioAPPController     Usuario                     { get; set; }
        

    }
}
