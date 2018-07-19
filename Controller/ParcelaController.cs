using System;
using System.Collections.Generic;


namespace Controller
{
    public class ParcelaController
    {
        public long         TB016_id                        { get; set; }
        public long         TB037_Id                        { get; set; }
        public double       TB002_ComissaoAdesao            { get; set; }
        public double       TB002_ComissaoMensalidade       { get; set; }
        public string       TB037_Negociador                { get; set; }
        public double       TB037_Comissao                  { get; set; }
        public int          TB016_ParcelasAgrupadas         { get; set; }
        public DateTime     TB012_Inicio                    { get; set; }
        public DateTime     TB012_Fim                       { get; set; }
        public int          NParcelasEmAtraso               { get; set; }
        public int          TB016_Parcela                   { get; set; }
        public int          TB016_TotalParcelas             { get; set; }
        public int          TB016_Entrada                   { get; set; }
        public int          TB016_EmitirBoleto              { get; set; }
        public string       TB016_NBoleto                   { get; set; }
        public int          TB016_ParcelaCancelamento       { get; set; }
        public string       TB016_NossoNumero               { get; set; }   
        public DateTime     TB016_Emissao                   { get; set; }
        public DateTime     TB016_Vencimento                { get; set; }
        public string       TB016_Pagador                   { get; set; }
        public string       TB016_CPFCNPJ                   { get; set; }
        public string       TB016_PagadorCEP                { get; set; }
        public string       TB016_PagadorCidade             { get; set; }
        public string       TB016_PagadorUF                 { get; set; }
        public string       TB016_EnderecoPagador           { get; set; }
        public double       TB016_Valor                     { get; set; }
        public double       TB016_Abatimento                { get; set; }
        public double       TB016_ValorPago                 { get; set; }
        public double       TB016_IOF                       { get; set; }
        public int          TB016_TipoVencimento            { get; set; }
        public string       TB016_EspecieDocumento          { get; set; }
        public string       TB016_BoletoDesc1               { get; set; }
        public string       TB016_BoletoDesc2               { get; set; }
        public string       TB016_BoletoDesc3               { get; set; }
        public string       TB016_BoletoDesc4               { get; set; }
        public string       TB016_BoletoDesc5               { get; set; }
        public string       TB016_Boleto                    { get; set; }
        public DateTime     TB016_DataPagamento             { get; set; }
        public string       TB016_Banco                     { get; set; }
        public string       TB016_Beneficiario              { get; set; }
        public string       TB016_BeneficiarioEndereco      { get; set; }
        public string       TB016_BeneficiarioCPFCNPJ       { get; set; }
        public string       TB016_BeneficiarioCidade        { get; set; }
        public string       TB016_BeneficiarioUF            { get; set; }
        public string       TB016_Agencia                   { get; set; }
        public string       TB016_ContaCorrente             { get; set; }
        public string       TB016_Carteira                  { get; set; }
        public Int32        TB012_CicloContrato             { get; set; }
        public double       TB016_ValorAdesao               { get; set; }
        public double       TB016_ValorMensalidades         { get; set; }
        public short        Index                           { get; set; }
        public DateTime     TB016_DataMovimentacao          { get; set; }
        public long         TB016_DocumentoBanco            { get; set; }
        public string       TB016_Modalidade                { get; set; }
        public string       TB016_BancoRecebedor            { get; set; }
        public string       TB016_AgenciaRecebedora         { get; set; }
        public double       TB016_ValorTitulo               { get; set; }
        public double       TB016_ValorIOF                  { get; set; }
        public double       TB016_ValorTarifa               { get; set; }
        public double       TB016_ValorBruto                { get; set; }
        public double       TB016_ValorOutrosDesconto       { get; set; }
        public double       TB016_Multa                     { get; set; }
        public double       TB016_Juros                     { get; set; }
        public double       TB016_ValorMulta                { get; set; }
        public double       TB016_ValorJuros                { get; set; }
        public long         DiasEmAtraso                    { get; set; }
        public int          TB016_CodigoMovimento           { get; set; }
        public DateTime     TB016_CadastradoEm              { get; set; }
        public long         TB016_CadastradoPor             { get; set; }
        public DateTime     TB016_AlteradoEm                { get; set; }
        public long         TB016_AlteradoPor               { get; set; }

        public int          TB016_NParcelasAtrazo           { get; set; }
        public int          TB016_FormaProcessamentoBaixa   { get; set; }
        /*  1 - Via Digitação Atendente; 
            2 - Via Digitação Financeiro; 
            3 - Via Interface Fisica;
            4 - Via Portal
            5 - Planilha de remessa
        */
        public string       TB016_CredNCartao                   { get; set; }
        public string       TB016_CredCPFTitularCartaoCartao    { get; set; }
        public string       TB016_CredNomeTitularCartaoCartao   { get; set; }
        public long         TB016_CredBandeira                  { get; set; }
        public int          TB016_CredNParcelas                 { get; set; }
        public double       TB016_CredValorParcelas             { get; set; }
        public string       TB016_CredAutorizacao               { get; set; }
        public string       TB016_CredCodValidador              { get; set; }
        public string       TB016_CredFormaParamentoDescricao   { get; set; }
        public long         TB016_CredFormaParamentoId          { get; set; }
        public bool         UnirParcela                         { get; set; }
        public DateTime     TB016_CredBaixaFeitaEm              { get; set; }
        public long         TB016_CredBaixaFeitaPor             { get; set; }
        public DateTime     TB016_CredDataCredito               { get; set; }
        public DateTime     TB016_CredDataCreditado             { get; set; }
        public long         TB016_CredConfirmacaoFeitaPor       { get; set; }
        public DateTime     TB016_CredConfirmacaoEm             { get; set; }
        public int          TB016_CredConfirmacaoForma          { get; set; }
        public double       TB016_CredValor                     { get; set; }
        public long         TB016_LoteExportacao                { get; set; }
        public string       TB016_ArquivoExportacao             { get; set; }
        public int          TB016_DiaFechamento                 { get; set; }
        public int          TB016_DiaVencimento                 { get; set; }
        public short        mes                                 { get; set; }
        public short        ano                                 { get; set; }
        public long         total                               { get; set; }
        public string       TB016_FormaPagamentoS               { get; set; }
        public string       DesParcela                          { get; set; }
        public enum TB016_FormaPagamentoE
        {
            Boleto          = 1,
            Dinheiro        = 2,
            Debito          = 3,
            //Cartao_2x   = 4,
            //Cartao_3x   = 5,
            Credito         = 6,
            MettaCard       = 7
        }
        public string TB016_StatusS { get; set; }
        public enum TB016_StatusE
        {
            Configurada     = 0,
            Gerada          = 1,
            Emitida         = 2,
            Cancelada       = 3,
            Vencida         = 4,
            Paga            = 5

        }
        public string TB016_TipoSacadoS { get; set; }
        public enum TB016_TipoSacadoE //Enum identico ao  PessoaController TB013_Tipo
        {
            Fisica          = 1,
            Juridica        = 2
        }

        /*TB032_BandeiraCartao*/
        public long TB032_Id { get; set; }
        public string TB032_BandeiraCartao { get; set; }

        /*TB033_PagamentoCartao*/

        public long TB033_Id { get; set; }
        public double TB033_Parcela { get; set; }
        public double TB033_ValorProgramado { get; set; }
        public short TB033_DataProgramada { get; set; }
        public double TB033_ValorRecebido { get; set; }
        public DateTime TB033_DataQuitacao { get; set; }
        public DateTime TB033_CadastradoEm { get; set; }
        public long TB033_CadastradoPor { get; set; }
        public DateTime TB033_AlteradoEm { get; set; }
        public long TB033_AlteradoPor { get; set; }
        public short TB033_FormaProcessamentoQuitacao { get; set; }
        public short TB033_Status { get; set; }

        /*TB031_FormaParamento*/
        public long TB031_Id { get; set; }
        public string TB031_Descricao { get; set; }
        public short TB031_Tipo { get; set; }
        public short TB031_TipoVencimento { get; set; }
        public short TB031_DVencimento { get; set; }
        public short TB031_NParcelas { get; set; }
        public double TB031_ValorMinimoParcela { get; set; }
        public short TB031_TipoTaxa { get; set; }
        public double TB031_Taxa { get; set; }
        public short TB031_Status { get; set; }



        public PlanoController Plano { get; set; }
        public long TB015_id { get; set; }
        public string TB015_Plano { get; set; }
        public EmpresaController Empresa { get; set; }
        public PessoaController Pessoa { get; set; }
        public ContratosController Contrato { get; set; }
        public Int64 TB012_id { get; set; }
        public MunicipioController Municipio { get; set; }
        public EstadoController Estado { get; set; }
        public BancoController Banco { get; set; }
        public List<ParcelaProdutosController> ParcelaProduto_L { get; set; }

        public ParcelaProdutosController Produto { get; set; }

        public PessoaController Titular { get; set; }

        public UnidadeController Unidade { get; set; }

    }
}
