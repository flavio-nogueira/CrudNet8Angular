using BlackEnd.Domain.Entities;
using BlackEnd.Domain.Interfaces;
using BlackEnd.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace BlackEnd.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly BlackEndContext _context;

        public ClienteRepository(BlackEndContext context)
        {
            _context = context;
        }
        public async Task<Cliente> AdicionarAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
        public async Task AtualizarAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }
        public async Task<Cliente> ObterPorIdAsync(Guid id)
        {
            return await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IEnumerable<Cliente>> ObterTodosAsync()
        {
            return await _context.Clientes.AsNoTracking().ToListAsync();
        }
        public async Task RemoverAsync(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> ExisteCpfCnpjAsync(string cpfCnpj)
        {
            return await _context.Clientes.AnyAsync(c => c.CpfCnpj == cpfCnpj);
        }
        public async Task<bool> ExisteEmailAsync(string email)
        {
            return await _context.Clientes.AnyAsync(c => c.Email == email);
        }
    }
}
