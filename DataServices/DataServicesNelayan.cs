using NelayanGo.Models; // Pastikan namespace ini benar
using Npgsql;
using System;

namespace NelayanGo.DataServices
{
    // Kelas Data Service untuk mengambil data dari PostgreSQL
    public class DataServiceNelayan
    {
        // 🚨 GANTI INI: Sesuaikan dengan string koneksi Anda
        private const string ConnectionString =
            "Host=localhost;Username=postgre_user;Password=postgre_password;Database=nelayan_go_db";

        // Mengambil data nelayan berdasarkan kode
        public Nelayan? GetDataNelayan(string kode)
        {
            Nelayan? nelayan = null;
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
                        nelayan = new Nelayan
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

                            TotalWaktuAktifHari = Convert.ToInt32(reader["TotalWaktuAktifHari"]),
                            TotalJarakBerlayarKM = Convert.ToInt32(reader["TotalJarakBerlayarKM"]),
                            TotalBeratTangkapanTON = Convert.ToInt32(reader["TotalBeratTangkapanTON"]),
                            TotalJumlahBerlayar = Convert.ToInt32(reader["TotalJumlahBerlayar"]),
                            TotalJamBerlayarJAM = Convert.ToInt32(reader["TotalJamBerlayarJAM"]),
                            TotalJenisTangkapan = Convert.ToInt32(reader["TotalJenisTangkapan"])
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