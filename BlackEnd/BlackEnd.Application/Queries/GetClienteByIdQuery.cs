using BlackEnd.Application.DTOs;
using MediatR;

namespace BlackEnd.Application.Queries;

public class GetClienteByIdQuery : IRequest<ClienteDto>
{
    public Guid Id { get; set; }

    public GetClienteByIdQuery(Guid id)
    {
        Id = id;
    }
}
