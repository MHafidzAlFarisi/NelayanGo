using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NelayanGo.Models;
using System.Windows;
using System.Collections.Generic; // Tambahkan ini

namespace NelayanGo.DataServices
{
    public class NelayanDataService
    {
        private static readonly HttpClient _http = new HttpClient();

        public NelayanDataService()
        {
            if (_http.BaseAddress == null)
            {
                if (!Uri.TryCreate($"{SupabaseConfig.Url}/rest/v1/", UriKind.Absolute, out var baseUri))
                    throw new InvalidOperationException($"Invalid SUPABASE_URL: {SupabaseConfig.Url}");

                _http.BaseAddress = baseUri;
            }

            if (!_http.DefaultRequestHeaders.Contains("apikey"))
                _http.DefaultRequestHeaders.Add("apikey", SupabaseConfig.Key);

            if (_http.DefaultRequestHeaders.Authorization == null)
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", SupabaseConfig.Key);
        }

        // --- FUNGSI INSERT (YANG SUDAH ADA) ---
        public async Task<bool> InsertProfilNelayan(NelayanModel nelayan)
        {
            try
            {
                var json = JsonSerializer.Serialize(nelayan);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Post, "user_profiles");
                request.Content = content;
                request.Headers.Add("Prefer", "return=representation");

                var response = await _http.SendAsync(request);

                if (response.IsSuccessStatusCode) return true;
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Gagal simpan: {error}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error koneksi: {ex.Message}");
                return false;
            }
        }

        // --- TAMBAHKAN FUNGSI GET INI ---
        public async Task<NelayanModel?> GetProfilByUserId(long userId)
        {
            try
            {
                // Filter berdasarkan user_id
                var url = $"user_profiles?user_id=eq.{userId}&limit=1";
                var response = await _http.GetAsync(url);

                if (!response.IsSuccessStatusCode) return null;

                var json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                // Supabase selalu mengembalikan Array/List, jadi kita deserialize ke List dulu
                var list = JsonSerializer.Deserialize<List<NelayanModel>>(json, options);

                if (list != null && list.Count > 0)
                {
                    return list[0]; // Ambil data pertama
                }

                return null; // Data tidak ditemukan
            }
            catch
            {
                return null;
            }
        }
    }
}