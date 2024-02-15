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
    public class UpsertHandler(IRepository repository) : IRequestHandler<UpsertCommand, AccountMe.Models.PositionHolding>
    {

        public async Task<AccountMe.Models.PositionHolding> Handle(UpsertCommand request, CancellationToken cancellationToken)
        {
            if (request.PositionHolding.Id > 0 && repository.GetByKey(typeof(Models.PositionHolding), request.PositionHolding.Id) != null)
            {
                return (AccountMe.Models.PositionHolding)await repository.Update(request.PositionHolding);
            }

            return (AccountMe.Models.PositionHolding)await repository.Insert(request.PositionHolding);
        }
    }
}
