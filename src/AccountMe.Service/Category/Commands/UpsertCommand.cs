using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Category.Commands
{
    public class UpsertCommand : IRequest <Models.Category>
    {

        public required Models.Category Category;

    }
}
