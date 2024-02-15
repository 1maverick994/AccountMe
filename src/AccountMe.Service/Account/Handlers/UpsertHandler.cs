using AccountMe.Service.Account.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Account.Handlers
{
    public class UpsertHandler(IRepository repository) : IRequestHandler<UpsertCommand, Models.Account>
    {

        public async Task<Models.Account> Handle(UpsertCommand request, CancellationToken cancellationToken)
        {
            if (request.Account.Id > 0 && repository.GetByKey(typeof(Models.Account), request.Account.Id) != null)
            {
                return (Models.Account)await repository.Update(request.Account);
            }

            return (Models.Account)await repository.Insert(request.Account);
        }
    }
}
