using BlackEnd.Domain.Enums;

namespace BlackEnd.Application.DTOs
{
    public class ClienteDto
    {
        public Guid Id { get; set; }
        public TipoPessoa Tipo { get; private set; }
        public string NomeRazaoSocial { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string CpfCnpj { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public string? InscricaoEstadual { get; set; }
        public bool IsentoIE { get; set; }
        public string Cep { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
