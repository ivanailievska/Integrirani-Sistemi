using System;
using System.Collections.Generic;
using System.Text;
using Tickets.Domain.Relations;

namespace Tickets.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<TicketsInShoppingCart> Tickets { get; set; }

        public double TotalPrice { get; set; }
    }
}
