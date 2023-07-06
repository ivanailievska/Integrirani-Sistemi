using System;
using System.Collections.Generic;
using System.Text;
using Tickets.Domain;
using Tickets.Domain.DomainModels;
using Tickets.Repository.Interface;
using Tickets.Service.Interface;

namespace Tickets.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<Order> getAllOrders()
        {
            return this._orderRepository.getAllOrders();
        }

        public Order getOrderDetails(BaseEntity model)
        {
            return this._orderRepository.getOrderDetails(model);
        }
    }
}
