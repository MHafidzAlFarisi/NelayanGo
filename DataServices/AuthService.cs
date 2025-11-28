using NelayanGo.Models;
using Npgsql;
using System;

namespace NelayanGo.DataServices
{
    public class AuthService
    {
        // samakan dengan yang di DataServiceNelayan
        private const string ConnectionString =
            "Host=localhost;Username=postgre_user;Password=postgre_password;Database=nelayan_go_db";

        public UserAccount? Login(string email, string password)
        {
            using var conn = new NpgsqlConnection(ConnectionString);

            const string sql = @"
                SELECT ""Id"", ""Nama"", ""Email"", ""Role""
                FROM ""TBL_USER""
                WHERE ""Email"" = @Email AND ""Password"" = @Password
                LIMIT 1;
            ";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);

            try
            {
                conn.Open();
                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new UserAccount
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nama = reader["Nama"].ToString() ?? string.Empty,
                        Email = reader["Email"].ToString() ?? string.Empty,
                        Role = reader["Role"].ToString() ?? string.Empty
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
                return null;
            }
        }
    }
}
