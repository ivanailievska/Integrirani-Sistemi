using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tickets.Domain.DomainModels;
using Tickets.Domain.DTO;
using Tickets.Domain.Relations;
using Tickets.Repository.Interface;
using Tickets.Service.Interface;

namespace Tickets.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;

        private readonly IRepository<TicketInOrder> _ticketInOrderRepository;
        private readonly IUserRepository _userRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<Order> orderRepository, IRepository<TicketInOrder> ticketInOrderRepository, IUserRepository userRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
            _ticketInOrderRepository = ticketInOrderRepository;
            _userRepository = userRepository;
        }

        public bool deleteTicketFromSoppingCart(string userId, Guid ticketId)
        {
            if (!string.IsNullOrEmpty(userId) && ticketId != null)
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                var itemToDelete = userShoppingCart.TicketsInShoppingCart.Where(z => z.TicketId.Equals(ticketId)).FirstOrDefault();

                userShoppingCart.TicketsInShoppingCart.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userShoppingCart);

                return true;
            }
            return false;
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userCard = loggedInUser.UserCart;

                var allProducts = userCard.TicketsInShoppingCart.ToList();

                var allProductPrices = allProducts.Select(z => new
                {
                    TicketPrice = z.CurrentTicket.Price,
                    Quantity = z.Quantity
                }).ToList();

                double totalPrice = 0.0;

                foreach (var item in allProductPrices)
                {
                    totalPrice += item.Quantity * item.TicketPrice;
                }

                var reuslt = new ShoppingCartDto
                {
                    Tickets = allProducts,
                    TotalPrice = totalPrice
                };

                return reuslt;
            }
            return new ShoppingCartDto();
        }

        public bool order(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userCard = loggedInUser.UserCart;

/*                EmailMessage message = new EmailMessage();

                message.MailTo = loggedInUser.Email;
                message.Subject = "Succesfully created order";
                message.Status = false;*/


                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepository.Insert(order);

                List<TicketInOrder> ticketInOrders = new List<TicketInOrder>();

                var result = userCard.TicketsInShoppingCart.Select(z => new TicketInOrder
                {
                    Id = Guid.NewGuid(),
                    TicketId = z.CurrentTicket.Id,
                    Ticket = z.CurrentTicket,
                    OrderId = order.Id,
                    Order = order,
                    Quantity = z.Quantity
                }).ToList();

                StringBuilder sb = new StringBuilder();

                sb.AppendLine("Your order is completed. The order contains: ");

                var totalPrice = 0.0;

                for (int i = 1; i < result.Count; i++)
                {
                    var item = result[i - 1];
                    totalPrice = item.Quantity * item.Ticket.Price;
                    sb.AppendLine(i.ToString() + ". " + item.Ticket.Name + " with price of: " + item.Ticket.Price + " and quantity of: " + item.Quantity.ToString());

                }

                sb.AppendLine("Total price: " + totalPrice.ToString());

                //message.Content = sb.ToString();

                ticketInOrders.AddRange(result);

                foreach (var item in ticketInOrders)
                {
                    this._ticketInOrderRepository.Insert(item);
                }

                loggedInUser.UserCart.TicketsInShoppingCart.Clear();

                //this._mailRepository.Insert(message);

                this._userRepository.Update(loggedInUser);

                return true;
            }

            return false;
        }
    }
}
