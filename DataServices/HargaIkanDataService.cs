using System;
using System.Collections.Generic;
using Npgsql;
using NelayanGo.Models;

namespace NelayanGo.DataServices
{
    public class HargaIkanDataService
    {
        // ==== READ ALL ====
        public List<HargaIkanModel> GetAll()
        {
            const string sql = @"
                SELECT 
                    ""kodeikan"",
                    ""TanggalUpdate"",
                    ""NamaIkan"",
                    ""HargaIkan"",
                    ""ID_Admin"",
                    ""Wilayah""
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
                    KodeIkan = reader.GetInt64(0),
                    TanggalUpdate = reader.GetDateTime(1),
                    NamaIkan = reader.GetString(2),
                    HargaIkan = Convert.ToDecimal(reader.GetValue(3)),
                    ID_Admin = reader.GetInt64(4),
                    Wilayah = reader.GetString(5)
                });
            }

            return list;
        }

        // ==== READ BY NAMA ====
        public HargaIkanModel? GetByNamaIkan(string namaIkan)
        {
            const string sql = @"
                SELECT 
                    ""kodeikan"",
                    ""TanggalUpdate"",
                    ""NamaIkan"",
                    ""HargaIkan"",
                    ""ID_Admin"",
                    ""Wilayah""
                FROM ""HargaIkan""
                WHERE LOWER(""NamaIkan"") = LOWER(@nama)
                ORDER BY ""TanggalUpdate"" DESC
                LIMIT 1;";

            using var conn = new NpgsqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("nama", namaIkan);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return null;

            return new HargaIkanModel
            {
                KodeIkan = reader.GetInt64(0),
                TanggalUpdate = reader.GetDateTime(1),
                NamaIkan = reader.GetString(2),
                HargaIkan = Convert.ToDecimal(reader.GetValue(3)),
                ID_Admin = reader.GetInt64(4),
                Wilayah = reader.GetString(5)
            };
        }

        // ==== INSERT ====
        public void Insert(HargaIkanModel model)
        {
            const string sql = @"
                INSERT INTO ""HargaIkan""
                    (""NamaIkan"", ""HargaIkan"", ""Wilayah"", ""TanggalUpdate"", ""ID_Admin"")
                VALUES
                    (@nama, @harga, @wilayah, @tgl, @admin);";

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

        // ==== UPDATE ====
        public void Update(HargaIkanModel model)
        {
            const string sql = @"
                UPDATE ""HargaIkan""
                SET 
                    ""NamaIkan""    = @nama,
                    ""HargaIkan""   = @harga,
                    ""Wilayah""     = @wilayah,
                    ""TanggalUpdate"" = @tgl
                WHERE ""kodeikan"" = @id;";

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

        // ==== DELETE ====
        public void Delete(long kodeIkan)
        {
            const string sql = @"
                DELETE FROM ""HargaIkan""
                WHERE ""kodeikan"" = @id;";

            using var conn = new NpgsqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", kodeIkan);

            cmd.ExecuteNonQuery();
        }
    }
}
