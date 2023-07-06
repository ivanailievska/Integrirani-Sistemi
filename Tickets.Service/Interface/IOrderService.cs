using System;
using System.Collections.Generic;
using System.Text;
using Tickets.Domain;
using Tickets.Domain.DomainModels;

namespace Tickets.Service.Interface
{
    public interface IOrderService
    {
        public List<Order> getAllOrders();
        public Order getOrderDetails(BaseEntity model);
    }
}
