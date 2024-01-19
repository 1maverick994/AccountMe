using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.User.Commands
{
    public class UpsertCommand : IRequest <Models.User>
    {

        public required Models.User User;

    }
}
