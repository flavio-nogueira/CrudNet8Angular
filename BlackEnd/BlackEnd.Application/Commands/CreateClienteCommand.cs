using BlackEnd.Domain.Enums;
using MediatR;

namespace BlackEnd.Application.Commands
{
    public class CreateClienteCommand : BaseClienteCommand, IRequest<Guid>
    {   
      
        public string CpfCnpj { get; init; } = string.Empty;
        public TipoPessoa Tipo { get; init; }
        public DateTime? DataNascimento { get; init; } 
        public string? InscricaoEstadual { get; init; }
        public bool? IsentoIE { get; init; }   
        public string Email { get; init; } = string.Empty;  
     
    }
}
