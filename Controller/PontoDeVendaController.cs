using System;

namespace Controller
{
    public class PontoDeVendaController
    {
        public long TB002_id
        { get; set; }
        public string TB002_Ponto
        { get; set; }
        public string Tb002FamiliarAdesaoFormaS
        { get; set; }
        public enum Tb002FamiliarAdesaoFormaE
        {
            Fixo = 1,
            Aliquota = 2
        }
        public double Tb002FamiliarAdesaoValor
        { get; set; }
        public double Tb002FamiliarAdesaoAliquota
        { get; set; }
        public string Tb002FamiliarMensalidadeFormaS
        { get; set; }
        public enum Tb002FamiliarMensalidadeFormaE
        {
            Fixo = 1,
            Aliquota = 2
        }
        public double Tb002FamiliarMensalidadeValor
        { get; set; }
        public double Tb002FamiliarMensalidadeAliquota
        { get; set; }
        public DateTime Tb002CadastradoEm
        { get; set; }
        public string Tb002CadastradoPor
        { get; set; }
        public long Tb002CadastradoPorI
        { get; set; }
        public DateTime Tb002AlteradoEm
        { get; set; }
        public string Tb002AlteradoPor
        { get; set; }
        public long Tb002AlteradoPorI
        { get; set; }
        public string Tb002StatusS
        { get; set; }
        public enum Tb002StatusE
        {
            Inativo = 0,
            Ativo = 1
        }
        public string Tb002ParceiroAdesaoFormaS
        { get; set; }
        public enum Tb002ParceiroAdesaoFormaE
        {
            Fixo = 1,
            Aliquota = 2
        }
        public double Tb002ParceiroAdesaoValor
        { get; set; }
        public double Tb002ParceiroAdesaoAliquota
        { get; set; }
        public string Tb002ParceiroMensalidadeFormaS
        { get; set; }
        public enum Tb002ParceiroMensalidadeFormaE
        {
            Fixo = 1,
            Aliquota = 2
        }
        public double Tb002ParceiroMensalidadeValor
        { get; set; }
        public double Tb002ParceiroMensalidadeAliquota
        { get; set; }
        public string Tb002CorporativoAdesaoFormaS
        { get; set; }
        public enum Tb002CorporativoAdesaoFormaE
        {
            Fixo = 1,
            Aliquota = 2
        }
        public double Tb002CorporativoAdesaoValor
        { get; set; }
        public double Tb002CorporativoAdesaoAliquota
        { get; set; }
        public string Tb002CorporativoMensalidadeFormaS
        { get; set; }
        public enum Tb002CorporativoMensalidadeFormaE
        {
            Fixo = 1,
            Aliquota = 2
        }
        public double Tb002CorporativoMensalidadeValor
        { get; set; }
        public double Tb002CorporativoMensalidadeAliquota
        { get; set; }
        public EmpresaController Empresa
        { get; set; }
    }
}
