using System.Collections.Generic;
using System.Threading.Tasks;
using AppTiengAnhBE.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;

namespace AppTiengAnhBE.Repositories.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private NpgsqlConnection CreateConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new NpgsqlConnection(connectionString);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<User>("SELECT * FROM users");
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM users WHERE id = @Id", new { Id = id });
        }

        public async Task<int> CreateUserAsync(User user)
        {
            using var connection = CreateConnection();
            var sql = @"
                INSERT INTO users (username, email, password, full_name, role_id) 
                VALUES (@Username, @Email, @Password, @FullName, @RoleId) 
                RETURNING Id";

            var id = await connection.ExecuteScalarAsync<int>(sql, new
            {
                user.username,
                user.email,
                user.password,
                user.full_name,
                user.role_id
            });
            return id;
        }

        public async Task<int> UpdateUserAsync(User user)
        {
            using var connection = CreateConnection();
            var sql = @"
                UPDATE users 
                SET 
                    username = @Username,
                    full_name = @Full_name,
                    email = @Email, 
                    password = @Password,
                    role_id = @Role_id
                WHERE id = @Id";
            return await connection.ExecuteAsync(sql, user);
        }

        public async Task<int> DeleteUserAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = "DELETE FROM users WHERE id = @Id";
            return await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
