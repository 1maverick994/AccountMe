using AccountMe.Service.Category.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Category.Handlers
{
    public class UpsertHandler(IRepository repository) : IRequestHandler<UpsertCommand, Models.Category>
    {

        public async Task<Models.Category> Handle(UpsertCommand request, CancellationToken cancellationToken)
        {
            if (request.Category.Id > 0 && repository.GetByKey(request.Category.Id) != null)
            {
                return (Models.Category)await repository.Update(request.Category);
            }

            return (Models.Category)await repository.Insert(request.Category);
        }
    }
}
