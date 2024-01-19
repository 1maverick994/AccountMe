using AccountMe.Service.Transaction.Commands;
using AccountMe.Service.Transaction.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Transaction.Handlers
{
    public class UpsertHandler(IRepository repository, Mediator mediator) : IRequestHandler<UpsertCommand, Models.Transaction>
    {

        public async Task<Models.Transaction> Handle(UpsertCommand request, CancellationToken cancellationToken)
        {
            Models.Transaction result;

            if (request.Transaction.Id > 0 && repository.GetByKey(request.Transaction.Id) != null)
            {
                result = (Models.Transaction)await repository.Update(request.Transaction);
            }
            else
            {
                result = (Models.Transaction)await repository.Insert(request.Transaction);
            }

            var domainEvent = new UpsertEvent() { TransactionId = result.Id };
            await mediator.Publish(domainEvent);

            return result;

        }
    }
}
