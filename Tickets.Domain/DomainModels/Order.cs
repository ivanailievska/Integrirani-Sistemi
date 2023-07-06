using System;
using System.Collections.Generic;
using System.Text;
using Tickets.Domain.Identity;
using Tickets.Domain.Relations;

namespace Tickets.Domain.DomainModels
{
    public class Order :BaseEntity
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public virtual ICollection<TicketInOrder> TicketsInOrder { get; set; }
    }
}
