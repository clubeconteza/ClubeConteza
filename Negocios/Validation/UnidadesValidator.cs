using Controller;
using FluentValidation;
using Negocios.Utilities.Format;
using Negocios.Utilities.Validation;

namespace Negocios.Validation
{
    public class UnidadesValidator : AbstractValidator<UnidadesModelController>
    {
        public UnidadesValidator()
        {
            RuleFor(v => v.Documento)
                .NotNull().NotEmpty().WithMessage("O campo CNPJ é obrigatório.")
                .Must(x => new CNPJValidator().IsValid(x)).WithMessage("O CNPJ é inválido!"); //Validar se já existe CNPJ Cadastrado

            RuleFor(v => v.NomeFantasia)
                .NotNull().NotEmpty().WithMessage("O campo Nome Fantasia é obrigatório.")
                .Length(0, 150).WithMessage("O campo Nome Fantasia deve ter até {MaxLength} caracteres.");

            RuleFor(v => v.RazaoSocial)
                .NotNull().NotEmpty().WithMessage("O campo Razão Social é obrigatório.")
                .Length(0, 150).WithMessage("O campo Razão Social deve ter até {MaxLength} caracteres.");

            RuleFor(v => v.Cep)
                .NotNull().NotEmpty().WithMessage("O campo CEP é obrigatório.")
                .Must(x => new CEPFormatter().CanBeFormatted(x)).WithMessage("O CEP é inválido!");

            RuleFor(v => v.Logradouro)
                .NotNull().NotEmpty().WithMessage("O campo Logradouro é obrigatório.")
                .Length(0, 150).WithMessage("O campo Logradouro deve ter até {MaxLength} caracteres.");

            RuleFor(v => v.Numero)
                .NotNull().NotEmpty().WithMessage("O campo Número é obrigatório.")
                .Length(0, 10).WithMessage("O campo Número deve ter até {MaxLength} caracteres.");

            RuleFor(v => v.Complemento)
                .NotNull().NotEmpty().WithMessage("O campo Complemento é obrigatório.")
                .Length(0, 50).WithMessage("O campo Complemento deve ter até {MaxLength} caracteres.");

            RuleFor(v => v.Bairro)
                .Length(0, 50).WithMessage("O campo Bairro deve ter até {MaxLength} caracteres.");

            RuleFor(v => v.IdMunicipio)
                .NotNull().NotEmpty().GreaterThan(0).WithMessage("O campo Município é obrigatório.");
        }
    }
}
