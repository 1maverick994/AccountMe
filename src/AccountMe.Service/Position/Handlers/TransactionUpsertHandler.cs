using AccountMe.Service.Transaction.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Position.Handlers
{
    internal class TransactionUpsertHandler : INotificationHandler<UpsertEvent>
    {
        public Task Handle(UpsertEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
