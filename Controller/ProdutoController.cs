
namespace Controller
{
    public class ProdutoController
    {
        //public Int64    LstID                   { get; set; }
        public long     TB014_id                { get; set; }
        public int      TB014_ValorMultiplo     { get; set; }
        public string   TB014_IdProtheus        { get; set; }
        public string   TB014_Produto           { get; set; }
        public double   TB014_ValorUnitario     { get; set; }
        public double   ValorTotal              { get; set; }
        public string   TB014_Descricao         { get; set; }
        public int      TB014_ValidoContezinos  { get; set; }
        public int      TB014_ValidoParceiros   { get; set; }
        public int      TB014_ValidoComporativo { get; set; }
        public int      TB014_Maiores           { get; set; }
        public int      TB014_Menores           { get; set; }
        public int      TB014_Isentos           { get; set; }
        public string   TB014_StatusS           { get; set; }
        public enum TB014_StatusE
        {
            Cadastradp  = 0,
            Ativo       = 1,
            Inativo     = 2
        }

        public string TB014_TipoS { get; set; }
        public enum TTB014_TipoE
        {
            Adesao = 1,
            Mensalidade = 2,
            Multa = 3,
            Negociação = 4,
            Serviço = 5
        }
    }
}
