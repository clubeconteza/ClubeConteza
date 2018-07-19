using System;

namespace Controller
{
    public class PagamentosController
    {
        public Int64    TB025_id                        { get; set; }
        public int      TB025_CodigoMovimento           { get; set; }
        public long TB025_NossoNumero                  { get; set; }
        public int      TB025_Tipo                      { get; set; }
        public Int64    TB016_id                        { get; set; }
        public Int64    TB025_DocumentoBanco            { get; set; }
        public int      TB025_Modalidade                { get; set; }
        public string   TB025_ContaCorrente             { get; set; }
        public DateTime TB025_Emissao                   { get; set; }
        public DateTime TB025_Vencimento                { get; set; }
        public double   TB025_ValorTitulo               { get; set; }
        public double   TB025_ValorAbatimento           { get; set; }
        public double   TB025_ValorIOF                  { get; set; }
        public double   TB025_ValorTarifa               { get; set; }
        public DateTime TB025_DataLiquidacao            { get; set; }     
        public DateTime TB025_DataMovimentacao          { get; set; }
        public string   TB025_BancoRecebedor            { get; set; }
        public string   TB025_AgenciaRecebedora         { get; set; }
        public double   TB025_ValorCobrado              { get; set; }
        public DateTime TB025_DataLiquidacaoCredito     { get; set; }
        public string   TB025_CPFCNPJ                   { get; set; }    
        public DateTime TB025_CadastradoEm              { get; set; }
        public Int64    TB025_CadastradoPor             { get; set; }
        public DateTime TB025_AlteradoEm                { get; set; }
        public Int64    TB025_AlteradoPor               { get; set; }
        public string   PAGADOR                         { get; set; }
        public string   STATUSPARCELA                   { get; set; } 
        public Int32    TB025_BancoOrigem               { get; set; }

        public string   TB025_FormaProcessamentoS { get; set; }
        public enum TB025_FormaProcessamentoE
        {
            Importação  = 1,
            Manual      = 2
        }
        public string TB025_FormaPagamentoS { get; set; }
        public enum TB025_FormaPagamentoE
        {
            Boleto      = 1,
            Dinheiro    = 2,
            Debito      = 3,
            Cartao_2x   = 4,
            Cartao_3x   = 5
        }

        /**/

        public Int64 TB012_id { get; set; }

        public string TB016_CPFCNPJ { get; set; }

        public string TB016_Pagador { get; set; }

        public ParcelaController Parcela { get; set; }

        public string TB016_StatusS { get; set; }
        public enum TB016_StatusE
        {
            Configurada = 0,
            Gerada = 1,
            Emitida = 2,
            Cancelada = 3,
            Vencida = 4,
            Panga = 5,

        }
    }
}