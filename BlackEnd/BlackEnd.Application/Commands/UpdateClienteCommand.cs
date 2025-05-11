using BlackEnd.Domain.Enums;
using MediatR;

namespace BlackEnd.Application.Commands
{
    public class UpdateClienteCommand : BaseClienteCommand, IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public TipoPessoa Tipo { get; init; }
        public string? InscricaoEstadual { get; set; }
        public bool IsentoIE { get; set; }
    }
}
