using System;


namespace Boleto.Controller
{
    public class ParametrosSaida
    {
        public int      Erro            { get; set; }
        public string   ErroDesc        { get; set; }
        public string   NumeroBoleto    { get; set; }
        public string   HTML            { get; set; }
        public string   BoletoCarne     { get; set; }
        public string   NossoNumero     { get; set; }
        public string   Agencia         { get; set; }
        public string   Conta           { get; set; }
        public string   Carteira        { get; set; }
    }
}