using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.PositionHolding.Commands
{
    public class UpsertCommand : IRequest <AccountMe.Models.PositionHolding>
    {

        public required AccountMe.Models.PositionHolding PositionHolding;

    }
}
