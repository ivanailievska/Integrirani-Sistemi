using System;
using System.Collections.Generic;
using System.Text;
using Tickets.Domain.Identity;

namespace Tickets.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(string id);
        void Insert(User entity);
        void Update(User entity);
        void Delete(User entity);
    }
}
