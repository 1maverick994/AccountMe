using AccountMe.Service.PositionHolding.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.PositionHolding.Handlers
{
    public class DeleteHandler(IRepository repository) : IRequestHandler<DeleteCommand>
    {


        public async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            await repository.Delete(request.PositionHolding);

        }
    }
}
