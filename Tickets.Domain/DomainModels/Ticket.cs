using System;
using System.Collections.Generic;
using System.Text;
using Tickets.Domain.Relations;

namespace Tickets.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public DateTime ValidUntil { get; set; }

        public string Image { get; set; }


        public virtual ICollection<TicketsInShoppingCart> TicketsInShoppingCart { get; set; }
        public virtual ICollection<TicketInOrder> TicketsInOrder { get; set; }

    }
}
