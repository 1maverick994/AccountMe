using AccountMe.Service.User.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.User.Handlers
{
    public class UpsertHandler(IRepository repository) : IRequestHandler<UpsertCommand, Models.User>
    {

        public async Task<Models.User> Handle(UpsertCommand request, CancellationToken cancellationToken)
        {
            if (request.User.Id > 0 && repository.GetByKey(request.User.Id) != null)
            {
                return (Models.User)await repository.Update(request.User);
            }

            return (Models.User)await repository.Insert(request.User);
        }
    }
}
