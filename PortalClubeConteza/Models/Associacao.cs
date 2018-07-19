using System.ComponentModel.DataAnnotations;

namespace PortalClubeConteza.Models
{
    public class Associacao
    {
        [Required(ErrorMessage = "O campo Primeiro nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
        [Phone(ErrorMessage = "O campo Telefone não é um número de telefone válido.")]
        public string Telefone { get; set; }

        [EmailAddress(ErrorMessage = "O campo Email não é um endereço de email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Plano escolhido é obrigatório.")]
        public string Tipo { get; set; }
    }
}