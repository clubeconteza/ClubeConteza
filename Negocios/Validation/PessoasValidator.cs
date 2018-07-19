using Controller;
using Controller.Enums;
using FluentValidation;
using Negocios.Utilities.Format;
using Negocios.Utilities.Validation;
using System;

namespace Negocios.Validation
{
    public class PessoasValidator : AbstractValidator<PessoasModelController>
    {
        public PessoasValidator()
        {
            RuleFor(v => v.CpfCnpj)
                .NotNull().NotEmpty().WithMessage("O campo CPF é obrigatório.")
                .Must(x => new CPFValidator().IsValid(x)).WithMessage("O CPF é inválido!"); //Validar se já existe CPF Cadastrado

            RuleFor(v => v.NomeCompleto)
                .NotNull().NotEmpty().WithMessage("O campo Nome Completo é obrigatório.");

            RuleFor(v => v.NomeExibicao)
                .NotNull().NotEmpty().WithMessage("O campo Nome Exibição é obrigatório.");

            RuleFor(v => v.Sexo)
                .NotNull().NotEmpty().Must(x => Enum.IsDefined(typeof(PessoasSexo), x)).WithMessage("O campo Sexo é obrigatório.");

            RuleFor(v => v.DataNascimento)
                .NotNull().NotEmpty().WithMessage("O campo Data Nascimento é obrigatório.");

            RuleFor(v => v.Rg)
                .NotNull().NotEmpty().WithMessage("O campo RG é obrigatório.")
                .Length(0, 50).WithMessage("O campo RG deve ter até {MaxLength} caracteres.");

            RuleFor(v => v.RgOrgaoEmissor)
                .NotNull().NotEmpty().WithMessage("O campo Órgão Emissor é obrigatório.")
                .Length(0, 10).WithMessage("O campo Órgão Emissor deve ter até {MaxLength} caracteres.");

            RuleFor(v => v.MaeNome)
                .NotNull().NotEmpty().WithMessage("O campo Nome da Mãe é obrigatório.");

            RuleFor(v => v.MaeDataNascimento)
                .LessThan(x => x.DataNascimento).WithMessage("A Data de Nacimento da Mãe deve ser inferior a Data de Nascimento do Titular!");

            RuleFor(v => v.PaiNome)
                .NotNull().NotEmpty().WithMessage("O campo Nome do Pai é obrigatório.");

            RuleFor(v => v.PaiDataNascimento)
                .LessThan(x => x.DataNascimento).WithMessage("A Data de Nacimento do Pai deve ser inferior a Data de Nascimento do Titular!");

            RuleFor(v => v.Cep)
                .NotNull().NotEmpty().GreaterThanOrEqualTo(0).WithMessage("O campo CEP do Titular é obrigatório.")
                .Must(x => new CEPFormatter().CanBeFormatted(x.ToString())).WithMessage("O CEP do Titular é inválido!");

            RuleFor(v => v.Logradouro)
                .NotNull().NotEmpty().WithMessage("O campo Logradouro do Titular é obrigatório.")
                .Length(0, 150).WithMessage("O campo Logradouro do Titular deve ter até {MaxLength} caracteres.");

            RuleFor(v => v.Numero)
                .NotNull().NotEmpty().WithMessage("O campo Número do Titular é obrigatório.")
                .Length(0, 10).WithMessage("O campo Número do Titular deve ter até {MaxLength} caracteres.");

            RuleFor(v => v.Complemento)
                .NotNull().NotEmpty().WithMessage("O campo Complemento do Titular é obrigatório.")
                .Length(0, 150).WithMessage("O campo Complemento do Titular deve ter até {MaxLength} caracteres.");

            RuleFor(v => v.Bairro)
                .Length(0, 150).WithMessage("O campo Bairro do Titular deve ter até {MaxLength} caracteres.");

            RuleFor(v => v.IdMunicipio)
                .NotNull().NotEmpty().GreaterThan(0).WithMessage("O campo Município do Titular é obrigatório.");
        }
    }
}
