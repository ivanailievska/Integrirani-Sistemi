using System;
using System.Collections.Generic;
using System.Text;
using Tickets.Domain;
using Tickets.Domain.DomainModels;

namespace Tickets.Repository.Interface
{
    public interface IOrderRepository
    {
        public List<Order> getAllOrders();
        public Order getOrderDetails(BaseEntity model);

    }
}
