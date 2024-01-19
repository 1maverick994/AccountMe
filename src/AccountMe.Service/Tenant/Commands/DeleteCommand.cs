using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Tenant.Commands
{
    public class DeleteCommand : IRequest 
    {

        public required Models.Tenant Tenant;

    }
}
