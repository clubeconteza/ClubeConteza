using System.ComponentModel;

namespace Controller.Enums
{
    public enum ContratosStatus
    {
        [Description("Cadastrado")]
        Cadastrado = 0,
        [Description("Ativo")]
        Ativo = 1,
        [Description("Bloqueado")]
        Bloqueado = 2,
        [Description("Inativo")]
        Inativo = 3,
        [Description("Inadimplente")]
        Inadimplente = 4,
        [Description("Cancelado")]
        Cancelado = 5,
        [Description("Negociado")]
        Negociado = 6
    }
}
