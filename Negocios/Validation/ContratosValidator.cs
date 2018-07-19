using Controller;
using FluentValidation;

namespace Negocios.Validation
{
    public class ContratosValidator : AbstractValidator<ContratosModelController>
    {
        public ContratosValidator()
        {
            RuleFor(v => v.Inicio)
                .NotNull().NotEmpty().WithMessage("O campo Início do Contrato é obrigatório.");

            RuleFor(v => v.Fim)
                .NotNull().NotEmpty().WithMessage("O campo Término do Contrato é obrigatório.")
                .GreaterThanOrEqualTo(x => x.Inicio.Value.AddYears(1)).WithMessage("A data de Término do Contrato deve ter no mínimo 12 meses a partir da data de Início do Contrato!");
        }
    }
}
