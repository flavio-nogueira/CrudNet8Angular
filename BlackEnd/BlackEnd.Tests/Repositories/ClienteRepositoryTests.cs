using BlackEnd.Domain.Entities;
using BlackEnd.Domain.Enums;
using BlackEnd.Infrastructure.Context;
using BlackEnd.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BlackEnd.Tests.Repositories
{
    public class ClienteRepositoryTests
    {
        private readonly DbContextOptions<BlackEndContext> _options;

        public ClienteRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<BlackEndContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
        }

        // Teste que verifica se o cliente PF é salvo corretamente
        [Fact]
        public async Task Add_ClientePF_Should_SaveCliente()
        {
            using var context = new BlackEndContext(_options);
            var repository = new ClienteRepository(context);

            // Criação de cliente PF com idade válida
            var clientePF = Cliente.CriarNovoCliente(
                "Cliente Teste", "12345678901", TipoPessoa.Fisica, DateTime.Now.AddYears(-20), null, null, "1234567890", "teste@email.com",
                "12345-678", "Rua Teste", "123", "Bairro Teste", "Cidade Teste", "Estado Teste"
            );
            await repository.AdicionarAsync(clientePF);
            await context.SaveChangesAsync();

            // Verificação se o cliente foi salvo corretamente
            var savedPF = await context.Clientes.FindAsync(clientePF.Id);
            savedPF.Should().NotBeNull();
        }

        // Teste que garante que PF menor de 18 anos lança exceção
        [Fact]
        public async Task Add_ClientePF_Under18_Should_ThrowException()
        {
            using var context = new BlackEndContext(_options);
            var repository = new ClienteRepository(context);

            var clientePFMenor = Cliente.CriarNovoCliente(
                "Cliente Teste Menor", "12345678902", TipoPessoa.Fisica, DateTime.Now.AddYears(-17), null, null, "1234567890", "teste2@email.com",
                "12345-678", "Rua Teste", "123", "Bairro Teste", "Cidade Teste", "Estado Teste"
            );

            Func<Task> actionPFMenor = async () =>
            {
                if (clientePFMenor.Tipo == TipoPessoa.Fisica && clientePFMenor.DataNascimento.HasValue)
                {
                    var idade = DateTime.Now.Year - clientePFMenor.DataNascimento.Value.Year;
                    if (idade < 18)
                        throw new Exception("Idade mínima para pessoa física é 18 anos.");
                }
                await repository.AdicionarAsync(clientePFMenor);
                await context.SaveChangesAsync();
            };
            await actionPFMenor.Should().ThrowAsync<Exception>().WithMessage("Idade mínima para pessoa física é 18 anos.");
        }

        // Teste que garante que PJ sem IE e não isento lança exceção
        [Fact]
        public async Task Add_ClientePJ_WithoutIE_Should_ThrowException()
        {
            using var context = new BlackEndContext(_options);
            var repository = new ClienteRepository(context);

            var clientePJ = Cliente.CriarNovoCliente(
                "Empresa Teste", "12345678901234", TipoPessoa.Juridica, null, null, false, "1234567890", "empresa@email.com",
                "12345-678", "Rua Teste", "123", "Bairro Teste", "Cidade Teste", "Estado Teste"
            );

            Func<Task> actionPJ = async () =>
            {
                if (clientePJ.Tipo == TipoPessoa.Juridica && !clientePJ.IsentoIE.GetValueOrDefault(false) && string.IsNullOrWhiteSpace(clientePJ.InscricaoEstadual))
                {
                    throw new Exception("Pessoa Jurídica deve informar IE ou declarar isenção.");
                }
                await repository.AdicionarAsync(clientePJ);
                await context.SaveChangesAsync();
            };
            await actionPJ.Should().ThrowAsync<Exception>().WithMessage("Pessoa Jurídica deve informar IE ou declarar isenção.");
        }

        // Teste que garante que CPF/CNPJ duplicado lança exceção
        [Fact]
        public async Task Add_Cliente_With_Duplicate_CpfCnpj_Should_ThrowException()
        {
            using var context = new BlackEndContext(_options);
            var repository = new ClienteRepository(context);

            // Criação de cliente inicial
            var cliente = Cliente.CriarNovoCliente(
                "Cliente Teste", "12345678901", TipoPessoa.Fisica, DateTime.Now.AddYears(-20), null, null, "1234567890", "teste@email.com",
                "12345-678", "Rua Teste", "123", "Bairro Teste", "Cidade Teste", "Estado Teste"
            );

            await repository.AdicionarAsync(cliente);
            await context.SaveChangesAsync();

            // Verificação de exceção para cliente duplicado
            Func<Task> action = async () =>
            {
                if (await context.Clientes.AnyAsync(c => c.CpfCnpj == cliente.CpfCnpj))
                {
                    throw new Exception("CPF/CNPJ já cadastrado.");
                }
                await repository.AdicionarAsync(cliente);
                await context.SaveChangesAsync();
            };
            await action.Should().ThrowAsync<Exception>().WithMessage("CPF/CNPJ já cadastrado.");
        }
    }
}
