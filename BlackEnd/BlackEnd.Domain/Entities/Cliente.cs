using BlackEnd.Domain.Enums;

namespace BlackEnd.Domain.Entities
{
    public class Cliente
    {
        public Guid Id { get; private set; }
        public string NomeRazaoSocial { get; private set; } = string.Empty;
        public string CpfCnpj { get; private set; } = string.Empty;
        public TipoPessoa Tipo { get; private set; }
        public DateTime? DataNascimento { get; private set; }
        public string? InscricaoEstadual { get; private set; }
        public bool? IsentoIE { get; private set; }
        public string? Telefone { get; private set; } = string.Empty;
        public string? Email { get; private set; } = string.Empty;
        public string? Cep { get; private set; } = string.Empty;
        public string? Endereco { get; private set; } = string.Empty;
        public string? Numero { get; private set; } = string.Empty;
        public string? Bairro { get; private set; } = string.Empty;
        public string? Cidade { get; private set; } = string.Empty;
        public string? Estado { get; private set; } = string.Empty;


        public static Cliente CriarNovoCliente(
            string nomeRazaoSocial,
            string cpfCnpj,
            TipoPessoa tipo,
            DateTime? dataNascimento,
            string? inscricaoEstadual,
            bool? isentoIE,
            string? telefone,
            string email,
            string cep,
            string endereco,
            string numero,
            string bairro,
            string cidade,
            string estado)
        {
            return new Cliente
            {
                Id = Guid.NewGuid(),
                NomeRazaoSocial = nomeRazaoSocial,
                CpfCnpj = cpfCnpj,
                Tipo = tipo,
                DataNascimento = dataNascimento,
                InscricaoEstadual = inscricaoEstadual,
                IsentoIE = isentoIE,
                Telefone = telefone,
                Email = email,
                Cep = cep,
                Endereco = endereco,
                Numero = numero,
                Bairro = bairro,
                Cidade = cidade,
                Estado = estado
            };
        }


        public void AtualizarDados(
            string nomeRazaoSocial,
            string? telefone,
            string email,
            string cep,
            string endereco,
            string numero,
            string bairro,
            string cidade,
            string estado,
            string? inscricaoEstadual,
            bool isentoIE)
        {
            NomeRazaoSocial = nomeRazaoSocial;
            Telefone = telefone;
            Email = email;
            Cep = cep;
            Endereco = endereco;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            InscricaoEstadual = inscricaoEstadual;
            IsentoIE = isentoIE;
        }
    }

}
