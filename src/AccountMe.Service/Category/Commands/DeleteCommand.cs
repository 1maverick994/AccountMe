using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Category.Commands
{
    public class DeleteCommand : IRequest 
    {

        public required Models.Category Category;

    }
}
