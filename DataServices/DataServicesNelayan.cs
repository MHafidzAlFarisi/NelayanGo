using NelayanGo.Models;
using Npgsql;
using System;

namespace NelayanGo.DataServices
{
    // Kelas Data Service untuk mengambil data dari PostgreSQL
    public class DataServiceNelayan
    {
        private const string ConnectionString =
            "Host=localhost;Username=postgre_user;Password=postgre_password;Database=nelayan_go_db";

        // Mengambil data nelayan berdasarkan kode
        public NelayanModel? GetDataNelayan(string kode)
        {
            NelayanModel? nelayan = null;
            string query = "SELECT * FROM TBL_NELAYAN WHERE \"KodeIdentik\" = @Kode";

            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@Kode", kode);

                try
                {
                    connection.Open();
                    NpgsqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        nelayan = new NelayanModel
                        {
                            Nama = reader["Nama"].ToString() ?? string.Empty,
                            KodeIdentik = reader["KodeIdentik"].ToString() ?? string.Empty,
                            Wilayah = reader["Wilayah"].ToString() ?? string.Empty,
                            Status = reader["Status"].ToString() ?? string.Empty,
                            NIK = reader["NIK"].ToString() ?? string.Empty,
                            TanggalLahir = reader["TanggalLahir"].ToString() ?? string.Empty,
                            TempatLahir = reader["TempatLahir"].ToString() ?? string.Empty,
                            NomorTelepon = reader["NomorTelepon"].ToString() ?? string.Empty,
                            Email = reader["Email"].ToString() ?? string.Empty,
                            Agama = reader["Agama"].ToString() ?? string.Empty,
                            GolonganDarah = reader["GolonganDarah"].ToString() ?? string.Empty,

                            // Note: numeric "Total*" fields are not defined on NelayanModel.
                            // Add them to Models/Nelayan.cs if you need to populate these values here.
                        };
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Log error ke console atau file log
                    Console.WriteLine($"Error fetching data: {ex.Message}");
                    return null;
                }
            }
            return nelayan;
        }
    }
}