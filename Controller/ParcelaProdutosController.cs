
namespace Controller
{
    public class ParcelaProdutosController
    {
        //public Int64    LstID               { get; set; }
        public long         TB017_id            { get; set; }
        public string       TB017_IdProteus     { get; set; }
        public string       TB017_Item          { get; set; }
        public double       TB017_ValorUnitario { get; set; }
        public double       TB017_ValorDesconto { get; set; }
        public double       TB017_ValorFinal    { get; set; }
        public long         TB016_Parcela       { get; set; }
        public int          TB017_Maior         { get; set; }
        public int          TB017_Menor         { get; set; }
        public int          TB017_Isento        { get; set; }
        public long         TB016_id            { get; set; }

        public string TB017_TipoS { get; set; }
        public enum TTB017_TipoE
        {
            Adesao          = 1,
            Mensalidade     = 2,
            Multa           = 3,
            Negociação      = 4,
            Serviço         = 5
        }

        public PessoaController Titular { get; set; }
        public ParcelaController Parcela { get; set; }
    }
}
