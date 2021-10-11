using dapper_demo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_demo.Repositories
{
    public interface IUserRepository
    {
        Task<IList<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> GetUserByUsername(string username);
        Task Create(User user);
    }
}
