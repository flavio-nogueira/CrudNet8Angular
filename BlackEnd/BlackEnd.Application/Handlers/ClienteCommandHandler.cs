using BlackEnd.Application.Commands;
using BlackEnd.Domain.Entities;
using BlackEnd.Domain.Interfaces;
using MediatR;

namespace BlackEnd.Application.Handlers
{
    public class ClienteCommandHandler :
        IRequestHandler<CreateClienteCommand, Guid>,
        IRequestHandler<UpdateClienteCommand, bool>,
        IRequestHandler<DeleteClienteCommand, bool>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<Guid> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            var novoCliente = Cliente.CriarNovoCliente(
                request.NomeRazaoSocial,
                request.CpfCnpj,
                request.Tipo,
                request.DataNascimento,
                request.InscricaoEstadual,
                request.IsentoIE,
                request.Telefone,
                request.Email,
                request.Cep,
                request.Endereco,
                request.Numero,
                request.Bairro,
                request.Cidade,
                request.Estado
            );

            await _clienteRepository.AdicionarAsync(novoCliente);
            return novoCliente.Id;
        }
        public async Task<bool> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
        {
            var clienteExistente = await _clienteRepository.ObterPorIdAsync(request.Id);
            if (clienteExistente == null) return false;

            clienteExistente.AtualizarDados(
                request.NomeRazaoSocial,
                request.Telefone,
                request.Email,
                request.Cep,
                request.Endereco,
                request.Numero,
                request.Bairro,
                request.Cidade,
                request.Estado,
                request.InscricaoEstadual,
                request.IsentoIE
            );

            await _clienteRepository.AtualizarAsync(clienteExistente);
            return true;
        }
        public async Task<bool> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
        {
            await _clienteRepository.RemoverAsync(request.Id);
            return true;
        }
    }
}
