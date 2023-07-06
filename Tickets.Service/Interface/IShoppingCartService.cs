using System;
using System.Collections.Generic;
using System.Text;
using Tickets.Domain.DTO;

namespace Tickets.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteTicketFromSoppingCart(string userId, Guid ticketId);
        bool order(string userId);
    }
}
