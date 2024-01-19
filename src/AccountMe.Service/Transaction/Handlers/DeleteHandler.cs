using AccountMe.Service.Transaction.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Transaction.Handlers
{
    public class DeleteHandler(IRepository repository) : IRequestHandler<DeleteCommand>
    {


        public async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            await repository.Delete(request.Transaction);

        }
    }
}
