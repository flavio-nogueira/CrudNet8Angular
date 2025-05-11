using BlackEnd.Application.Commands;
using BlackEnd.Domain.Enums;
using FluentValidation;

namespace BlackEnd.Application.Validators
{
    public class UpdateClienteCommandValidator : AbstractValidator<UpdateClienteCommand>
    {
        public UpdateClienteCommandValidator()
        {
            RuleFor(x => x.NomeRazaoSocial)
                .NotEmpty().WithMessage("Nome/Razão Social é obrigatório.")
                .MaximumLength(200).WithMessage("Nome/Razão Social deve ter no máximo 200 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Formato de email inválido.");

            RuleFor(x => x.InscricaoEstadual)
                .NotEmpty().When(x => x.Tipo == TipoPessoa.Juridica && !x.IsentoIE)
                .WithMessage("Inscrição Estadual é obrigatória para pessoa jurídica.");

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID do cliente é obrigatório.");
        }
    }
}
