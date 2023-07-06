using System;
using System.Collections.Generic;
using System.Text;
using Tickets.Domain.Identity;
using Tickets.Domain.Relations;

namespace Tickets.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public virtual ICollection<TicketsInShoppingCart> TicketsInShoppingCart { get; set; }

    }
}
