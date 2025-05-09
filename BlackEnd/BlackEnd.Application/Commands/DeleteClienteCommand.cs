using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackEnd.Application.Commands
{
    public class DeleteClienteCommand : IRequest<bool>
    {
        public Guid Id { get; init; }
    }
}
