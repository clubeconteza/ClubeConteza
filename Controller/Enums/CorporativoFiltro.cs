using System.ComponentModel;

namespace Controller.Enums
{
    public enum CorporativoFiltro
    {
        [Description("Todos")]
        Todos = 0,
        [Description("Contrato")]
        Contrato = 1,
        [Description("Nome Fantasia")]
        NomeFantasia = 2,
        [Description("CNPJ")]
        Cnpj = 3,
        [Description("Status")]
        Status = 4
    }
}
