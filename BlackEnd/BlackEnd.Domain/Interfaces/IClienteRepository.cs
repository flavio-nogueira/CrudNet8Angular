using BlackEnd.Domain.Entities;

namespace BlackEnd.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ObterTodosAsync();
        Task<Cliente> ObterPorIdAsync(Guid id);
        Task<Cliente> AdicionarAsync(Cliente cliente);
        Task AtualizarAsync(Cliente cliente);
        Task RemoverAsync(Guid id);
        Task<bool> ExisteCpfCnpjAsync(string cpfCnpj);
        Task<bool> ExisteEmailAsync(string email);
    }
}
