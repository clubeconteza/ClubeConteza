using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boleto.Controller
{
    public class BancoController
    {
        public Int64    TB018_id            { get; set; }
        public string   TB018_url           { get; set; }
        public string   TB018_Agencia       { get; set; }
        public string   TB018_ContaCorrente { get; set; }
        public string   TB018_Cartao        { get; set; }
        public string   TB018_Cliente       { get; set; }
        public string   TB018_chaveAcesso   { get; set; }     
        public int      TB018_Tipo          { get; set; }
        public int      TB018_Banco         { get; set; }
        public Int64    TB018_EmpresaId     { get; set; }
    }
}