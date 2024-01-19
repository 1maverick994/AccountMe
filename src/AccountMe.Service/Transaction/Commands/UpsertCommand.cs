using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Transaction.Commands
{
    public class UpsertCommand : IRequest <Models.Transaction>
    {

        public required Models.Transaction Transaction;

    }
}
