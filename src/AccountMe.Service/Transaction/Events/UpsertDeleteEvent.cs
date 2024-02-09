using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Transaction.Events
{
    public class UpsertDeleteEvent : INotification
    {

        public int? TransactionId { get; set; }

        public int? PositionInId { get; set; }

        public int? PositionOutId { get; set; }

        public decimal? Amount { get; set; }
        
        public TransactionType? TransactionType { get; set; }

        public int? PreviousPositionInId { get; set; }

        public int? PreviousPositionOutId { get; set; }

        public decimal? PreviousAmount { get; set; }

        public TransactionType? PreviousTransactionType { get; set; }
        

    }
}
