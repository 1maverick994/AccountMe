using AccountMe.Service.Position.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Position.Handlers
{
    public class UpsertHandler(IRepository repository) : IRequestHandler<UpsertCommand, Models.Position>
    {

        public async Task<Models.Position> Handle(UpsertCommand request, CancellationToken cancellationToken)
        {
            if (request.Position.Id > 0 && repository.GetByKey(typeof(Models.Position), request.Position.Id) != null)
            {
                return (Models.Position)await repository.Update(request.Position);
            }

            return (Models.Position)await repository.Insert(request.Position);
        }
    }
}
