using System;

namespace Controller
{
    public class ContratosModelController
    {
        public long Id { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public long NumeroDaSorte { get; set; }
    }
}
