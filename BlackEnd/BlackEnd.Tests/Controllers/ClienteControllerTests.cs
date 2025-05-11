using AutoMapper;
using BlackEnd.API.Controllers;
using BlackEnd.Application.Commands;
using BlackEnd.Application.DTOs;
using BlackEnd.Domain.Entities;
using BlackEnd.Domain.Interfaces;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace BlackEnd.Tests.Controllers
{
    public class ClienteControllerTests
    {
        // Mocks para dependências
        private readonly Mock<IClienteRepository> _clienteRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IMediator> _mediatorMock = new();
        private readonly Mock<IValidator<CreateClienteCommand>> _createValidatorMock = new();
        private readonly Mock<IValidator<UpdateClienteCommand>> _updateValidatorMock = new();

        private readonly ClienteController _controller;

        public ClienteControllerTests()
        {
            // Configuração do controlador com dependências mockadas
            _controller = new ClienteController(
                _clienteRepositoryMock.Object,
                _mapperMock.Object,
                _mediatorMock.Object,
                _createValidatorMock.Object,
                _updateValidatorMock.Object);
        }

        [Fact]
        public async Task Create_ValidCommand_ReturnsCreatedResult()
        {
            // Arrange: Configura o comando e simula a validação bem-sucedida
            var command = new CreateClienteCommand();
            _createValidatorMock.Setup(v => v.ValidateAsync(command, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(Guid.NewGuid());

            // Act: Executa o método Create do controlador
            var result = await _controller.Create(command);

            // Assert: Verifica se o resultado é CreatedAtActionResult
            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task Create_InvalidCommand_ReturnsBadRequest()
        {
            // Arrange: Configura o comando com falha na validação
            var command = new CreateClienteCommand();
            _createValidatorMock.Setup(v => v.ValidateAsync(command, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult(new[] { new FluentValidation.Results.ValidationFailure("Nome", "Nome é obrigatório.") }));

            // Act: Executa o método Create do controlador
            var result = await _controller.Create(command);

            // Assert: Verifica se o resultado é BadRequestObjectResult
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetById_ExistingClient_ReturnsOk()
        {
            // Arrange: Simula um cliente existente
            var cliente = new Cliente();
            _clienteRepositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(cliente);
            _mapperMock.Setup(m => m.Map<ClienteDto>(cliente)).Returns(new ClienteDto());

            // Act: Executa o método GetById do controlador
            var result = await _controller.GetById(Guid.NewGuid());

            // Assert: Verifica se o resultado é OkObjectResult
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetById_NonExistingClient_ReturnsNotFound()
        {
            // Arrange: Simula um cliente inexistente
            _clienteRepositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync((Cliente)null);

            // Act: Executa o método GetById do controlador
            var result = await _controller.GetById(Guid.NewGuid());

            // Assert: Verifica se o resultado é NotFoundObjectResult
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Update_ValidCommand_ReturnsOk()
        {
            // Arrange: Configura o comando e simula a validação bem-sucedida
            var command = new UpdateClienteCommand { Id = Guid.NewGuid() };
            _updateValidatorMock.Setup(v => v.ValidateAsync(command, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(true);

            // Act: Executa o método Update do controlador
            var result = await _controller.Update(command.Id, command);

            // Assert: Verifica se o resultado é OkResult
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task Delete_ExistingClient_ReturnsNoContent()
        {
            // Arrange: Simula a exclusão bem-sucedida
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteClienteCommand>(), default)).ReturnsAsync(true);

            // Act: Executa o método Delete do controlador
            var result = await _controller.Delete(Guid.NewGuid());

            // Assert: Verifica se o resultado é NoContentResult
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task GetAll_ReturnsListOfClientes()
        {
            // Arrange: Simula uma lista de clientes
            var clientes = new List<Cliente> { new Cliente() };
            _clienteRepositoryMock.Setup(r => r.ObterTodosAsync()).ReturnsAsync(clientes);
            _mapperMock.Setup(m => m.Map<List<ClienteDto>>(clientes)).Returns(new List<ClienteDto> { new ClienteDto() });

            // Act: Executa o método GetAll do controlador
            var result = await _controller.GetAll();

            // Assert: Verifica se o resultado é OkObjectResult
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
