using System.ComponentModel.DataAnnotations;

namespace PortalClubeConteza.Models
{
    public class AlteraSenha
    {
        [Required(ErrorMessage = "O campo Senha atual é obrigatório.")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "O campo Nova senha é obrigatório.")]
        public string SenhaNova { get; set; }

        [Required(ErrorMessage = "O campo Confirme a nova senha é obrigatório.")]
        public string ConfirmaSenha { get; set; }
    }
}