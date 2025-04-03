using EuroTruck.AuthFeature.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace EuroTruck.AuthFeature.Services
{
    public class AuthService
    {
        private readonly string connectionString = "Server=localhost;Port=5432;Database=truck-company;User Id=postgres;Password=postgres;";

        public async Task<bool> RegisterAsync(User user)
        {
            using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();
            string sql = "INSERT INTO users (username, password) VALUES (@username, @password)";
            using var cmd = new NpgsqlCommand(sql, conn);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            cmd.Parameters.AddWithValue("username", user.Username);
            cmd.Parameters.AddWithValue("password", hashedPassword);
            await cmd.ExecuteNonQueryAsync();
            return true;
        }

        public async Task<bool> LoginAsync(User user)
        {
            using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();
            string sql = "SELECT password FROM users WHERE username = @username";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("username", user.Username);
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                string storedHash = reader.GetString(0);
                return BCrypt.Net.BCrypt.Verify(user.Password, storedHash);
            }
            return false;
        }
    }
}
