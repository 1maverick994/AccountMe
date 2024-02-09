using AccountMe.Service.Position.Commands;
using AccountMe.Service.Transaction.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Position.Handlers
{
    public class TransactionUpsertDeleteHandler(IRepository repository, IMediator mediator) : INotificationHandler<UpsertDeleteEvent>
    {
        public async Task Handle(UpsertDeleteEvent notification, CancellationToken cancellationToken)
        {

            if (notification.PreviousTransactionType.HasValue && notification.PreviousAmount.HasValue)
            {
                // Rollback previous version of transaction

                if (notification.PreviousPositionInId.HasValue)
                {
                    var position = (Models.Position)await repository.GetByKey(notification.PreviousPositionInId.Value);
                    if (position != null)
                    {
                        position.Balance -= notification.PreviousAmount.Value;
                        var upsertCommand = new UpsertCommand()
                        {
                            Position = position
                        };
                        await mediator.Send(upsertCommand);
                    }
                }

                if (notification.PreviousPositionOutId.HasValue)
                {
                    var position = (Models.Position)await repository.GetByKey(notification.PreviousPositionOutId.Value);
                    if (position != null)
                    {
                        position.Balance += notification.PreviousAmount.Value;
                        var upsertCommand = new UpsertCommand()
                        {
                            Position = position
                        };
                        await mediator.Send(upsertCommand);
                    }
                }
            }

            if (notification.TransactionType.HasValue && notification.Amount.HasValue)
            {
                // Commit new transaction

                if (notification.PositionInId.HasValue)
                {
                    var position = (Models.Position)await repository.GetByKey(notification.PositionInId.Value);
                    if (position != null)
                    {
                        position.Balance += notification.Amount.Value;
                        var upsertCommand = new UpsertCommand()
                        {
                            Position = position
                        };
                        await mediator.Send(upsertCommand);
                    }
                }

                if (notification.PositionOutId.HasValue)
                {
                    var position = (Models.Position)await repository.GetByKey(notification.PositionOutId.Value);
                    if (position != null)
                    {
                        position.Balance -= notification.Amount.Value;
                        var upsertCommand = new UpsertCommand()
                        {
                            Position = position
                        };
                        await mediator.Send(upsertCommand);
                    }
                }
            }

        }
    }
}
