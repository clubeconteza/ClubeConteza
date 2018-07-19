using System.ComponentModel;

namespace Controller.Enums
{
    public enum UnidadesStatus
    {
        [Description("Cadastrado")]
        Cadastrado = 0,
        [Description("Ativo")]
        Ativo = 1,
        [Description("Inativo")]
        Inativo = 2
    }
}
