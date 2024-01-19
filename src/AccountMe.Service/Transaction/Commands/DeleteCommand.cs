using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Transaction.Commands
{
    public class DeleteCommand : IRequest 
    {

        public required Models.Transaction Transaction;

    }
}
