using Dapper;
using dapper_demo.Context;
using dapper_demo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace dapper_demo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDapperContext _context;
        public UserRepository(IDapperContext context)
        {
            this._context = context;
        }
        
        public async Task Create(string username)
        {
            string query = "INSERT INTO Users(username) VALUES (@username)";
            using (var connection = _context.CreateConnection())
            {
                int rowsAffected = await connection.ExecuteAsync(query, new { username = username });
            }                
        }

        public async Task<User> GetUserById(int id)
        {
            string query = "SELECT * FROM Users WHERE Id = @userId";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleAsync<User>(query, new { userId = id });
                return user;
            }                
        }

        public async Task<User> GetUserByUsername(string username)
        {
            string query = "SELECT * FROM Users WHERE Username = @username";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<User>(query, new { username = username });
                return user;
            }
        }

        public async Task<IList<User>> GetUsers()
        {
            List<User> users = new List<User>();
            string query = "SELECT * FROM Users";
            using (var connection = _context.CreateConnection())
            {
                users = (await connection.QueryAsync<User>(query)).ToList();
            }
            return users;
        }
    }
}
