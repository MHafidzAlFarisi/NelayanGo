using System;
using System.Collections.Generic;
using Npgsql;
using NelayanGo.Models;

namespace NelayanGo.DataServices
{
    public class IkanTangkapanDataService
    {
        // --- READ: semua data ---
        public List<IkanTangkapanModel> GetAll()
        {
            const string sql = @"
                SELECT *
                FROM ""IkanTangkapan""
                ORDER BY ""JamTangkap"" DESC;";

            return ExecuteSelect(sql, null);
        }

        // --- READ: khusus 1 user ---
        public List<IkanTangkapanModel> GetByUser(long userId)
        {
            const string sql = @"
                SELECT *
                FROM ""IkanTangkapan""
                WHERE ""ID_User"" = @userId
                ORDER BY ""JamTangkap"" DESC;";

            var param = new NpgsqlParameter("userId", userId);
            return ExecuteSelect(sql, param);
        }

        // --- READ: 1 user & 1 tanggal ---
        public List<IkanTangkapanModel> GetByUserAndDate(long userId, DateTime date)
        {
            const string sql = @"
                SELECT *
                FROM ""IkanTangkapan""
                WHERE ""ID_User"" = @userId
                  AND CAST(""JamTangkap"" AS DATE) = @tgl
                ORDER BY ""JamTangkap"" DESC;";

            var pUser = new NpgsqlParameter("userId", userId);
            var pTgl = new NpgsqlParameter("tgl", date.Date);

            return ExecuteSelect(sql, pUser, pTgl);
        }

        // Helper umum untuk SELECT
        private List<IkanTangkapanModel> ExecuteSelect(string sql, params NpgsqlParameter[]? parameters)
        {
            var list = new List<IkanTangkapanModel>();

            using var conn = new NpgsqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(sql, conn);
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                // Urutan kolom (sesuaikan dengan tabel Supabase):
                // 0: kodetangkapan
                // 1: created_at
                // 2: NamaIkan
                // 3: BeratKg
                // 4: TotalHargaIkan
                // 5: kode_ikan
                // 6: JamTangkap
                // 7: Lokasi
                // 8: ID_User

                var item = new IkanTangkapanModel
                {
                    kodetangkapan = reader.IsDBNull(0) ? 0 : reader.GetInt64(0),
                    TanggalInput = reader.IsDBNull(1) ? DateTime.MinValue : reader.GetDateTime(1),
                    NamaIkan = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                    BeratKg = reader.IsDBNull(3) ? 0 : Convert.ToInt32(reader.GetValue(3)),
                    TotalHargaIkan = reader.IsDBNull(4) ? 0 : reader.GetInt64(4),
                    KodeIkan = reader.IsDBNull(5) ? 0 : reader.GetInt64(5),
                    JamTangkap = reader.IsDBNull(6) ? DateTime.MinValue : reader.GetDateTime(6),
                    Lokasi = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                    ID_User = reader.IsDBNull(8) ? 0 : reader.GetInt64(8),

                    // kalau model-mu punya field kode_ikan terpisah:
                    kode_ikan = reader.IsDBNull(5) ? 0 : reader.GetInt64(5)
                };

                list.Add(item);
            }

            return list;
        }

        // --- INSERT ---
        public void Insert(IkanTangkapanModel model)
        {
            const string sql = @"
                INSERT INTO ""IkanTangkapan""
                    (""created_at"", ""NamaIkan"", ""Lokasi"", ""JamTangkap"",
                     ""BeratKg"", ""TotalHargaIkan"", ""kode_ikan"", ""ID_User"")
                VALUES
                    (@created_at, @nama, @lokasi, @jam,
                     @beratkg, @totalharga, @kodeikan, @id_user);";

            using var conn = new NpgsqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("created_at", model.TanggalInput);
            cmd.Parameters.AddWithValue("nama", model.NamaIkan);
            cmd.Parameters.AddWithValue("lokasi", model.Lokasi);
            cmd.Parameters.AddWithValue("jam", model.JamTangkap);
            cmd.Parameters.AddWithValue("beratkg", model.BeratKg);
            cmd.Parameters.AddWithValue("totalharga", model.TotalHargaIkan);
            cmd.Parameters.AddWithValue("kodeikan", model.KodeIkan);
            cmd.Parameters.AddWithValue("id_user", model.ID_User);

            cmd.ExecuteNonQuery();
        }

        // --- UPDATE ---
        public void Update(IkanTangkapanModel model)
        {
            const string sql = @"
                UPDATE ""IkanTangkapan""
                SET ""NamaIkan""       = @nama,
                    ""Lokasi""         = @lokasi,
                    ""JamTangkap""     = @jam,
                    ""BeratKg""        = @beratkg,
                    ""TotalHargaIkan"" = @totalharga,
                    ""kode_ikan""      = @kodeikan
                WHERE ""kodetangkapan"" = @id
                  AND ""ID_User""       = @id_user;";

            using var conn = new NpgsqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("nama", model.NamaIkan);
            cmd.Parameters.AddWithValue("lokasi", model.Lokasi);
            cmd.Parameters.AddWithValue("jam", model.JamTangkap);
            cmd.Parameters.AddWithValue("beratkg", model.BeratKg);
            cmd.Parameters.AddWithValue("totalharga", model.TotalHargaIkan);
            cmd.Parameters.AddWithValue("kodeikan", model.KodeIkan);
            cmd.Parameters.AddWithValue("id", model.kodetangkapan);
            cmd.Parameters.AddWithValue("id_user", model.ID_User);

            cmd.ExecuteNonQuery();
        }

        // --- DELETE ---
        public void Delete(long kodeTangkapan, long userId)
        {
            const string sql = @"
                DELETE FROM ""IkanTangkapan""
                WHERE ""kodetangkapan"" = @id
                  AND ""ID_User""       = @id_user;";

            using var conn = new NpgsqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", kodeTangkapan);
            cmd.Parameters.AddWithValue("id_user", userId);

            cmd.ExecuteNonQuery();
        }
    }
}
