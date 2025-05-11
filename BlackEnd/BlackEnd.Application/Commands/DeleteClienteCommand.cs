using MediatR;

namespace BlackEnd.Application.Commands
{
    public class DeleteClienteCommand : IRequest<bool>
    {
        public Guid Id { get; init; }
    }
}
