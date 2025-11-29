using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NelayanGo.Models;
using Npgsql;
using System.Collections.Generic;

namespace NelayanGo.DataServices
{
    public class HargaIkanDataService
    {
        private const string ConnectionString =
            "Host=db.hnamnhkbtnvbowmreddz.supabase.co;" +
            "Port=5432;" +
            "Username=postgres;" +
            "Password=CsPTsy1wSr9Aejn5;" +
            "Database=postgres;" +
            "SslMode=Require;Trust Server Certificate=true;";

        public List<HargalkanModel> GetAll()
        {
            const string sql = @"
            SELECT ""KodeIkan"", ""NamaIkan"", ""Hargalkan"", ""Wilayah"", ""TanggalUpdate""
            FROM ""Hargalkan""
            ORDER BY ""TanggalUpdate"" DESC";

            var result = new List<HargalkanModel>();

            using var conn = new NpgsqlConnection(ConnectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new HargalkanModel
                {
                    KodeIkan = (int)reader.GetInt64(reader.GetOrdinal("KodeIkan")),
                    NamaIkan = reader["NamaIkan"]?.ToString() ?? "",
                    // kolom Hargalkan = bigint, convert ke decimal
                    HargaIkan = Convert.ToDecimal(reader["Hargalkan"]),
                    Wilayah = reader["Wilayah"]?.ToString() ?? "",
                    TanggalUpdate = reader.GetDateTime(reader.GetOrdinal("TanggalUpdate"))
                });
            }

            return result;
        }

        public void Insert(HargalkanModel model, long adminId)
        {
            const string sql = @"
            INSERT INTO ""Hargalkan""
            (""NamaIkan"", ""Hargalkan"", ""Wilayah"", ""TanggalUpdate"", ""ID_Admin"")
            VALUES (@nama, @harga, @wilayah, @tanggal, @idAdmin);";

            using var conn = new NpgsqlConnection(ConnectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("nama", model.NamaIkan);
            cmd.Parameters.AddWithValue("harga", Convert.ToInt64(model.HargaIkan));
            cmd.Parameters.AddWithValue("wilayah", model.Wilayah);
            cmd.Parameters.AddWithValue("tanggal", model.TanggalUpdate);
            cmd.Parameters.AddWithValue("idAdmin", adminId);

            cmd.ExecuteNonQuery();
        }

        public void Update(HargalkanModel model)
        {
            const string sql = @"
            UPDATE ""Hargalkan""
            SET ""NamaIkan"" = @nama,
                ""Hargalkan"" = @harga,
                ""Wilayah"" = @wilayah,
                ""TanggalUpdate"" = @tanggal
            WHERE ""KodeIkan"" = @id;";

            using var conn = new NpgsqlConnection(ConnectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("nama", model.NamaIkan);
            cmd.Parameters.AddWithValue("harga", Convert.ToInt64(model.HargaIkan));
            cmd.Parameters.AddWithValue("wilayah", model.Wilayah);
            cmd.Parameters.AddWithValue("tanggal", model.TanggalUpdate);
            cmd.Parameters.AddWithValue("id", model.KodeIkan);

            cmd.ExecuteNonQuery();
        }

        public void Delete(long id)
        {
            const string sql = @"DELETE FROM ""Hargalkan"" WHERE ""KodeIkan"" = @id;";

            using var conn = new NpgsqlConnection(ConnectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }

    }
}


