using System.ComponentModel.DataAnnotations;

namespace PortalClubeConteza.Models
{
    public class CadastraSenha
    {
        [Required(ErrorMessage = "O campo Cpf é obrigatório.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O campo Cpf deve ser uma cadeia de caracteres com 11 de comprimento.")]
        public string Cpf { get; set; }
    }
}