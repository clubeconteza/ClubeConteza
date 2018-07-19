using System;

namespace Controller
{
    public class ComissaoController
    {

        public long Tb016Id { get; set; }
        public long Tb012Id { get; set; }

        public long Tb011Id { get; set; }
        public long Tb035Id { get; set; }
        public long Tb002Id { get; set; }
        public DateTime Tb035DataReferencia { get; set; }

        public double Tb035FamiliarAdesao { get; set; }

        public double Tb035FamiliarMensalidade { get; set; }

        public double Tb035ParceiroAdesao { get; set; }

        public double Tb035ParceiroMensalidade { get; set; }

        public double Tb035CorporativoAdesao { get; set; }

        public double Tb035CorporativoMensalidade { get; set; }


        public string Tb035StatusS { get; set; }

        public enum Tb035StatusE
        {
            Processado = 1,
            Pago = 2
        }


    }
}
