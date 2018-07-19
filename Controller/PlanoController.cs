using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controller
{
    public class PlanoController
    {
        public Int64    TB015_id                    { get; set; }
        public double   TB015_IOF                   { get; set; }
        public int      TB015_TipoVencimento        { get; set; }
        public string   TB015_EspecieDocumento      { get; set; }
        public string   TB015_BoletoDesc1           { get; set; }
        public string   TB015_BoletoDesc2           { get; set; }
        public string   TB015_BoletoDesc3           { get; set; }
        public string   TB015_BoletoDesc4           { get; set; }
        public string   TB015_BoletoDesc5           { get; set; }
        public int      TB015_LiberadoCPF           { get; set; }
        public int      TB015_LiberadoCNPJ          { get; set; }
        public string   TB015_Plano                 { get; set; }
        public string   TB015_Descricao             { get; set; }    
        public double   TB015_ValorAdesao           { get; set; }
        public double   ValorTotalProdutos          { get; set; }       
        public int      TB015_PermiteAbonarAdesao   { get; set; }
        public int      TB015_Ciclo                 { get; set; }
        public int      TB015_Contezinos            { get; set; }
        public int      TB015_Parceiros             { get; set; }
        public int      TB015_Corporativo           { get; set; }
        public int      TB015_Maiores               { get; set; }
        public int      TB015_Menores               { get; set; }
        public int      TB015_Isentos               { get; set; }
        public DateTime TB015_Inicio                { get; set; }
        public DateTime TB015_Fim                   { get; set; }
        public string   TB015_StatusS               { get; set; }
        public enum     TB015_StatusE
        {
            Cadastradp  = 0,
            Ativo       = 1,
            Inativo     = 2
        }

        public PontoDeVendaController   PontoDeVenda    { get; set; }
        public List<ProdutoController>  PlanoProduto_L  { get; set; }
        public ParcelaController        Parcela         { get; set; }

    }
}
