using BlackEnd.Application.DTOs;
using BlackEnd.Application.Queries;
using BlackEnd.Domain.Interfaces;
using MediatR;

namespace BlackEnd.Application.Handlers;

public class ClienteQueryHandler : IRequestHandler<GetClienteByIdQuery, ClienteDto>
{
    private readonly IClienteRepository _repository;

    public ClienteQueryHandler(IClienteRepository repository)
    {
        _repository = repository;
    }
    public async Task<ClienteDto> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
    {
        var cliente = await _repository.ObterPorIdAsync(request.Id);
        if (cliente == null)
            throw new KeyNotFoundException("Cliente não encontrado.");

        return new ClienteDto
        {
            Id = cliente.Id,
            NomeRazaoSocial = cliente.NomeRazaoSocial,
            Email = cliente.Email,
            Telefone = cliente.Telefone
        };
    }
}
