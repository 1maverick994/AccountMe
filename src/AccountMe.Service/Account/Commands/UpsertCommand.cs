using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Account.Commands
{
    public class UpsertCommand : IRequest <Models.Account>
    {

        public required Models.Account Account;

    }
}
