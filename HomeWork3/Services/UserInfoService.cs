using Dapper;
using HomeWork3.Models;
using HomeWork3.Services.Interfaces;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace HomeWork3.Services
{
    public class UserInfoService : IUserInfoService
    {
        private const string ConnectionString = "host=localhost;port=5432;database=postgres;username=postgres;password=12345";

        public async Task<User> GetById(Guid id)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return await connection.QuerySingleAsync<User>
                    ("SELECT * FROM users WHERE Id = @id", new { id });
            }
        }
    }
}
