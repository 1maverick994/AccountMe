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
    public class UpsertHandler(IRepository repository, IMediator mediator) : IRequestHandler<UpsertCommand, Models.Transaction>
    {

        public async Task<Models.Transaction> Handle(UpsertCommand request, CancellationToken cancellationToken)
        {
            Models.Transaction result;
            var domainEvent = new UpsertDeleteEvent();                      

            if (request.Transaction.Id > 0 && repository.GetByKey(typeof(Models.Transaction), request.Transaction.Id) != null)
            {            
                var currentTr = (Models.Transaction)await repository.GetByKey(typeof(Models.Transaction), request.Transaction.Id);
                domainEvent.PreviousTransactionType = currentTr.Type;
                domainEvent.PreviousPositionInId = currentTr.PositionIn?.Id;
                domainEvent.PreviousPositionOutId = currentTr.PositionOut?.Id;
                domainEvent.PreviousAmount = currentTr.Amount;

                result = (Models.Transaction)await repository.Update(request.Transaction);
            }
            else
            {
                result = (Models.Transaction)await repository.Insert(request.Transaction);
            }

            domainEvent.TransactionId = result.Id;
            domainEvent.TransactionType = result.Type;
            domainEvent.PositionInId = result.PositionIn?.Id;
            domainEvent.PositionOutId = result.PositionOut?.Id;
            domainEvent.Amount = result.Amount;

            await mediator.Publish(domainEvent);

            return result;

        }
    }
}
