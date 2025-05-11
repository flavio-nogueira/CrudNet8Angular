using BlackEnd.Application.Commands;
using BlackEnd.Application.Validators;
using BlackEnd.Domain.Enums;
using BlackEnd.Domain.Interfaces;
using FluentValidation.TestHelper;
using Moq;

namespace BlackEnd.Tests.Validators
{
    public class ClienteValidatorTests
    {
        private readonly CreateClienteCommandValidator _createValidator;
        private readonly UpdateClienteCommandValidator _updateValidator;

        public ClienteValidatorTests()
        {
            var clienteRepositoryMock = new Mock<IClienteRepository>();
            clienteRepositoryMock.Setup(repo => repo.ExisteCpfCnpjAsync(It.IsAny<string>())).ReturnsAsync(false);
            clienteRepositoryMock.Setup(repo => repo.ExisteEmailAsync(It.IsAny<string>())).ReturnsAsync(false);

            _createValidator = new CreateClienteCommandValidator(clienteRepositoryMock.Object);
            _updateValidator = new UpdateClienteCommandValidator();
        }

        // Teste que garante que um comando de criação de cliente válido passa sem erros
        [Fact]
        public async Task CreateClienteCommand_Valid_Should_Pass()
        {
            var command = new CreateClienteCommand
            {
                NomeRazaoSocial = "Cliente Teste",
                CpfCnpj = "12345678901",
                Tipo = TipoPessoa.Fisica,
                DataNascimento = DateTime.Now.AddYears(-20),
                Email = "teste@email.com"
            };

            var result = await _createValidator.TestValidateAsync(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        // Teste que garante que um comando com CPF inválido falha na validação
        [Fact]
        public async Task CreateClienteCommand_Invalid_CPF_Should_Fail()
        {
            var command = new CreateClienteCommand
            {
                NomeRazaoSocial = "Cliente Teste",
                CpfCnpj = "123",
                Tipo = TipoPessoa.Fisica,
                DataNascimento = DateTime.Now.AddYears(-20),
                Email = "teste@email.com"
            };

            var result = await _createValidator.TestValidateAsync(command);
            result.ShouldHaveValidationErrorFor(c => c.CpfCnpj);
        }

        // Teste que garante que um comando de atualização válido passa sem erros
        [Fact]
        public void UpdateClienteCommand_Valid_Should_Pass()
        {
            var command = new UpdateClienteCommand
            {
                NomeRazaoSocial = "Cliente Atualizado",
                Email = "atualizado@email.com",
                Id = Guid.NewGuid(),
                Tipo = TipoPessoa.Juridica,
                IsentoIE = true
            };

            var result = _updateValidator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        // Teste que garante que um comando de atualização com e-mail inválido falha na validação
        [Fact]
        public void UpdateClienteCommand_Invalid_Email_Should_Fail()
        {
            var command = new UpdateClienteCommand
            {
                NomeRazaoSocial = "Cliente Atualizado",
                Email = "invalido",
                Id = Guid.NewGuid(),
                Tipo = TipoPessoa.Juridica,
                IsentoIE = true
            };

            var result = _updateValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Email);
        }
    }
}
