using Dapper;
using HomeWork3.Models;
using HomeWork3.Services.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace HomeWork3.Services
{
    public class UpdateInfoService : IUpdateUserInfo
    {
        private const string ConnectionString = "host=localhost;port=5432;database=postgres;username=postgres;password=12345";

        public Task Update(User user)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                 return connection.ExecuteScalarAsync<User>
                    ("UPDATE public.users SET fname = @fname, lname = @lname, phone = @phone, email = @email WHERE id = @id", user);
            }
        }
    }
}
