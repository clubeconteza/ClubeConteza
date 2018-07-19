using System.Collections.Generic;

namespace Boleto.Controller
{
    public class AcessoUsuarioController
    {
        public string CpfCnpjUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public List<string> CnpjPlanos { get; set; }

        public int tb034ImpContezinos { get; set; }
    }
}