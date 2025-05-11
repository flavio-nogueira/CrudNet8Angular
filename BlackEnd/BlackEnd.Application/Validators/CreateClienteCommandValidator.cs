using BlackEnd.Application.Commands;
using BlackEnd.Domain.Enums;
using BlackEnd.Domain.Interfaces;
using FluentValidation;
using System.Globalization;

namespace BlackEnd.Application.Validators
{
    public class CreateClienteCommandValidator : AbstractValidator<CreateClienteCommand>
    {
        private readonly IClienteRepository _clienteRepository;

        public CreateClienteCommandValidator(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;

            RuleFor(c => c.Tipo)
           .IsInEnum()
           .WithMessage("O campo 'Tipo' deve ser 'Fisica' ou 'Juridica'.");

            // Validação de CPF/CNPJ - Único e Obrigatório
            RuleFor(x => x.CpfCnpj)
                .NotEmpty().WithMessage("O campo CPF/CNPJ é obrigatório.")
                .Length(11, 14).WithMessage("CPF/CNPJ deve ter entre 11 e 14 caracteres.")
                .MustAsync(async (cpf, cancellation) => await CpfCnpjUnico(cpf))
                .WithMessage("CPF/CNPJ já está cadastrado.");

            // Validação de E-mail - Único e Obrigatório
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O campo E-mail é obrigatório.")
                .EmailAddress().WithMessage("O campo E-mail é inválido.")
                .MustAsync(async (email, cancellation) => await EmailUnico(email))
                .WithMessage("E-mail já está cadastrado.");

            // Validação de Tipo de Pessoa (Física ou Jurídica)
            RuleFor(x => x.Tipo)
                .NotNull().WithMessage("O campo Tipo é obrigatório.")
                .IsInEnum().WithMessage("Tipo de pessoa inválido.");

            // Regras para Pessoa Física
            When(x => x.Tipo == TipoPessoa.Fisica, () =>
            {
                RuleFor(x => x.DataNascimento)
                    .NotEmpty().WithMessage("O campo Data de Nascimento é obrigatório para pessoa física.")
                    .Must(data => data.HasValue && CalcularIdade(data.Value) >= 18)
                    .WithMessage("A idade mínima para pessoa física é 18 anos.");

                RuleFor(x => x.InscricaoEstadual)
                    .Empty().WithMessage("O campo Inscrição Estadual não é permitido para pessoa física.");

                RuleFor(x => x.IsentoIE)
                    .Must(x => x == null).WithMessage("O campo IsentoIE não é permitido para pessoa física.");
            });

            // Regras para Pessoa Jurídica
            When(x => x.Tipo == TipoPessoa.Juridica, () =>
            {
                RuleFor(x => x.InscricaoEstadual)
                    .NotEmpty().WithMessage("O campo Inscrição Estadual é obrigatório para pessoa jurídica.")
                    .Unless(x => x.IsentoIE == true)
                    .WithMessage("Se o campo IsentoIE for falso, a Inscrição Estadual é obrigatória.");

                RuleFor(x => x.IsentoIE)
                    .NotNull().WithMessage("O campo IsentoIE é obrigatório para pessoa jurídica.");
            });
        }

        // Método para Verificar Unicidade do CPF/CNPJ
        private async Task<bool> CpfCnpjUnico(string cpfCnpj)
        {
            return !await _clienteRepository.ExisteCpfCnpjAsync(cpfCnpj);
        }

        // Método para Verificar Unicidade do E-mail
        private async Task<bool> EmailUnico(string email)
        {
            return !await _clienteRepository.ExisteEmailAsync(email);
        }

        private bool ValidarData(string data)
        {
            return DateTime.TryParseExact(data, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        // Método para Calcular Idade para Pessoa Física
        private int CalcularIdade(DateTime dataNascimento)
        {
            var hoje = DateTime.UtcNow.Date;
            var idade = hoje.Year - dataNascimento.Year;
            if (dataNascimento.Date > hoje.AddYears(-idade)) idade--;
            return idade;
        }
    }
}
