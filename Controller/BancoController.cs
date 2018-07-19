using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controller
{
    public class BancoController
    {
        public long     TB018_id            { get; set; }
        public Int16    TB018_Banco         { get; set; }
        public Int16    TB018_Tipo          { get; set; }
        public string   TB018_url           { get; set; }
        public string   TB018_ContaCorrente { get; set; }
        public string   TB018_Cartao        { get; set; }
        public long     TB018_Cliente       { get; set; }
        public string   TB018_chaveAcesso   { get; set; }
        public Int16    TB018_CNB           { get; set; }
      
        public long TB018_EmpresaId { get; set; }
    }
}
