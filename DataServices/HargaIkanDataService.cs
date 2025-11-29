using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NelayanGo.Models;

namespace NelayanGo.DataServices
{
    public class HargaIkanDataService
    {
        // READ ALL
        public List<HargaIkanModel> GetAll()
        {
            const string sql = @"
                SELECT ""KodeIkan"", ""NamaIkan"", ""HargaIkan"",
                       ""Wilayah"", ""TanggalUpdate"", ""ID_Admin""
                FROM ""HargaIkan""
                ORDER BY ""TanggalUpdate"" DESC;";

            var list = new List<HargaIkanModel>();

            using var conn = new NpgsqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new HargaIkanModel
                {
                    KodeIkan = reader.GetInt64(reader.GetOrdinal("KodeIkan")),
                    NamaIkan = reader["NamaIkan"]?.ToString() ?? "",
                    HargaIkan = Convert.ToDecimal(reader["HargaIkan"]),
                    Wilayah = reader["Wilayah"]?.ToString() ?? "",
                    TanggalUpdate = reader.GetDateTime(reader.GetOrdinal("TanggalUpdate")),
                    ID_Admin = reader.GetInt64(reader.GetOrdinal("ID_Admin"))
                });
            }

            return list;
        }

        // INSERT
        public void Insert(HargaIkanModel model)
        {
            const string sql = @"
                INSERT INTO ""HargaIkan""
                (""NamaIkan"", ""HargaIkan"", ""Wilayah"", ""TanggalUpdate"", ""ID_Admin"")
                VALUES (@nama, @harga, @wilayah, @tgl, @admin);";

            using var conn = new NpgsqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("nama", model.NamaIkan);
            cmd.Parameters.AddWithValue("harga", Convert.ToInt64(model.HargaIkan));
            cmd.Parameters.AddWithValue("wilayah", model.Wilayah);
            cmd.Parameters.AddWithValue("tgl", model.TanggalUpdate);
            cmd.Parameters.AddWithValue("admin", model.ID_Admin);

            cmd.ExecuteNonQuery();
        }

        // UPDATE
        public void Update(HargaIkanModel model)
        {
            const string sql = @"
                UPDATE ""HargaIkan""
                SET ""NamaIkan"" = @nama,
                    ""HargaIkan"" = @harga,
                    ""Wilayah"" = @wilayah,
                    ""TanggalUpdate"" = @tgl
                WHERE ""KodeIkan"" = @id;";

            using var conn = new NpgsqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("nama", model.NamaIkan);
            cmd.Parameters.AddWithValue("harga", Convert.ToInt64(model.HargaIkan));
            cmd.Parameters.AddWithValue("wilayah", model.Wilayah);
            cmd.Parameters.AddWithValue("tgl", model.TanggalUpdate);
            cmd.Parameters.AddWithValue("id", model.KodeIkan);

            cmd.ExecuteNonQuery();
        }

        // DELETE
        public void Delete(long kodeIkan)
        {
            const string sql = @"DELETE FROM ""HargaIkan"" WHERE ""KodeIkan"" = @id;";

            using var conn = new NpgsqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", kodeIkan);
            cmd.ExecuteNonQuery();
        }
    }
}





