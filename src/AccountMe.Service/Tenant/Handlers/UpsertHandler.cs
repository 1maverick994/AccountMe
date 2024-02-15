using AccountMe.Service.Tenant.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Tenant.Handlers
{
    public class UpsertHandler(IRepository repository) : IRequestHandler<UpsertCommand, Models.Tenant>
    {

        public async Task<Models.Tenant> Handle(UpsertCommand request, CancellationToken cancellationToken)
        {
            if (request.Tenant.Id > 0 && repository.GetByKey(typeof(Models.Tenant), request.Tenant.Id) != null)
            {
                return (Models.Tenant)await repository.Update(request.Tenant);
            }

            return (Models.Tenant)await repository.Insert(request.Tenant);
        }
    }
}
