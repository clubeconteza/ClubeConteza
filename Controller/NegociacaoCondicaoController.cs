

namespace Controller
{
    public class NegociacaoCondicaoController
    {
      
        public long TB036_Id                            { get; set; }
        public string TB036_Nome                        { get; set; }
        public int TB036_NParcelasMinimo                { get; set; }
        public int TB036_NParcelasMaximo                { get; set; }
        public double TB036_DescontoMultaAdesao         { get; set; }
        public double TB036_DescontoJurosAdesao         { get; set; }
        public double TB036_DescontoMultaMensalidade    { get; set; }
        public double TB036_DescontoJurosMensalidade    { get; set; }
        public double TB036_DescontoParcela             { get; set; }

        public double TB036_DescontoAdesao              { get; set; }
        
        public double TB036_ValorMinimoParcela          { get; set; }
        public string TB036_Descricao { get; set; }
        public string TB036_StatusS                     { get; set; }
        public enum TB036_StatusE
        {
            Inativo = 0,
            Ativo = 1
        }

    }
}
