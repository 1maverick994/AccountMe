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
    public class DeleteHandler(IRepository repository, IMediator mediator) : IRequestHandler<DeleteCommand>
    {


        public async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var domainEvent = new UpsertDeleteEvent();

            if (request.Transaction.Id > 0 && repository.GetByKey(request.Transaction.Id) != null)
            {
                var currentTr = (Models.Transaction)await repository.GetByKey(request.Transaction.Id);
                domainEvent.PreviousTransactionType = currentTr.Type;
                domainEvent.PreviousPositionInId = currentTr.PositionIn?.Id;
                domainEvent.PreviousPositionOutId = currentTr.PositionOut?.Id;
                domainEvent.PreviousAmount = currentTr.Amount;
            }          

            await mediator.Publish(domainEvent);
            await repository.Delete(request.Transaction);

        }
    }
}
