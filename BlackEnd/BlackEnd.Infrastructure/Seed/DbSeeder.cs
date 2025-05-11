using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BlackEnd.Domain.Entities;
using BlackEnd.Domain.Enums;
using BlackEnd.Infrastructure.Context;

namespace BlackEnd.Infrastructure.Seed
{
    public static class DbSeeder
    {
        private static readonly string[] Nomes =
        {
            "João Silva", "Maria Oliveira", "Carlos Pereira", "Ana Costa",
            "Fernando Lima", "Julia Santos", "Roberto Nogueira", "Beatriz Martins",
            "Bruno Souza", "Mariana Almeida", "Paulo Henrique", "Larissa Melo",
            "Ricardo Lopes", "Camila Vasconcelos", "Sergio Farias", "Tatiana Mendes",
            "Gabriel Dias", "Bianca Figueiredo", "Rodrigo Sampaio", "Alice Castro"
        };

        private static readonly string[] NomesEmpresas =
        {
            "Tech Solutions LTDA", "Alpha Comércio LTDA", "Beta Serviços LTDA",
            "Gamma Indústria LTDA", "Delta Consulting LTDA", "Omega Foods LTDA",
            "Zeta Transportes LTDA", "Epsilon Software LTDA", "Lambda Finanças LTDA",
            "Sigma Marketing LTDA"
        };

        private static readonly string[] Bairros =
        {
            "Campo Limpo", "Jardim Miriam", "Pinheiros", "Santo Amaro", "Centro", "Inocop"
        };

        private static readonly string[] Ruas =
        {
            "Rua Professora Nina Stocco", "Avenida João Dias", "Avenida Santo Amaro",
            "Rua Efigenia", "Rua Diogo Martin"
        };

        private static readonly string[] Cidades =
        {
            "São Paulo", "Rio de Janeiro", "Campinas", "Santos",
            "Belo Horizonte", "Curitiba", "Porto Alegre",
            "Brasília", "Goiânia", "Salvador"
        };

        private static readonly string[] Estados =
        {
            "SP", "RJ", "MG", "RS", "PR"
        };

        private static readonly Random Random = new Random();

        public static void SeedClientes(BlackEndContext context)
        {
            if (!context.Clientes.Any())
            {
                var clientes = new List<Cliente>();

                for (int i = 0; i < 40; i++)
                {
                    var isPessoaFisica = Random.Next(0, 2) == 0;
                    var nome = isPessoaFisica
                        ? Nomes[Random.Next(Nomes.Length)]
                        : NomesEmpresas[Random.Next(NomesEmpresas.Length)];

                    var email = isPessoaFisica
                        ? $"{nome.ToLower().Replace(" ", ".")}@capitanigroup.com"
                        : $"{nome.ToLower().Replace(" ", ".").Replace("ltda", "").Replace(",", "")}@empresarial.com";

                    clientes.Add(Cliente.CriarNovoCliente(
                        nomeRazaoSocial: nome,
                        cpfCnpj: isPessoaFisica ? GerarCpfUnico(i) : GerarCnpjUnico(i),
                        tipo: isPessoaFisica ? TipoPessoa.Fisica : TipoPessoa.Juridica,
                        dataNascimento: isPessoaFisica ? DateTime.Now.AddYears(-Random.Next(18, 60)) : (DateTime?)null,
                        inscricaoEstadual: isPessoaFisica ? null : (Random.Next(0, 2) == 0 ? $"IE{Random.Next(1000, 9999)}" : null),
                        isentoIE: isPessoaFisica ? (bool?)null : (Random.Next(0, 2) == 1),
                        telefone: $"119{Random.Next(10000000, 99999999)}",
                        email: email,
                        cep: $"{Random.Next(10000, 99999)}-{Random.Next(100, 999)}",
                        endereco: $"{Ruas[Random.Next(Ruas.Length)]} {Random.Next(1, 200)}",
                        numero: Random.Next(1, 500).ToString(),
                        bairro: Bairros[Random.Next(Bairros.Length)],
                        cidade: Cidades[Random.Next(Cidades.Length)],
                        estado: Estados[Random.Next(Estados.Length)]
                    ));
                }

                context.Clientes.AddRange(clientes);
                context.SaveChanges();
                Console.WriteLine("✔️ SeedClientes executado com sucesso.");
            }
            else
            {
                Console.WriteLine("✔️ SeedClientes já está preenchido. Nenhum cliente foi gerado.");
            }
        }

        private static string GerarCpfUnico(int index) => $"123456789{index:D2}";
        private static string GerarCnpjUnico(int index) => $"12345678000{index:D2}";
    }
}
