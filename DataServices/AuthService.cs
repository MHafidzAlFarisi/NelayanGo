using NelayanGo.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace NelayanGo.DataServices
{
    public class AuthService
    {
        private static readonly HttpClient _http = new HttpClient();

        public AuthService()
        {
            // Set base URL Supabase REST
            _http.BaseAddress ??= new Uri($"{SupabaseConfig.SupabaseUrl}/rest/v1/");

            // Header umum Supabase
            if (!_http.DefaultRequestHeaders.Contains("apikey"))
            {
                _http.DefaultRequestHeaders.Add("apikey", SupabaseConfig.SupabaseAnonKey);
            }

            if (_http.DefaultRequestHeaders.Authorization == null)
            {
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", SupabaseConfig.SupabaseAnonKey);
            }
        }

        // ===================== REGISTER =====================
        public UserAccount? Register(string username, string email, string password, string role)
        {
            // Hash password dulu (pakai SHA256 sederhana)
            var passwordHash = HashPassword(password);

            var payload = new
            {
                username = username,
                email = email,
                password_hash = passwordHash,
                role = role
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // request POST ke /rest/v1/users
            var request = new HttpRequestMessage(HttpMethod.Post, "users");
            request.Content = content;
            request.Headers.Add("Prefer", "return=representation"); // Supaya balikan row yang baru

            HttpResponseMessage response;
            try
            {
                response = _http.Send(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HTTP error register: {ex.Message}");
                return null;
            }

            var respBody = response.Content.ReadAsStringAsync().Result;

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Supabase error register: {response.StatusCode} - {respBody}");
                return null;
            }

            // Supabase balikin array JSON, ambil elemen pertama
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var users = JsonSerializer.Deserialize<List<SupabaseUserDto>>(respBody, options);

            var u = users != null && users.Count > 0 ? users[0] : null;
            if (u == null) return null;

            return new UserAccount
            {
                Id = u.id,
                Username = u.username ?? "",
                Email = u.email ?? "",
                Role = u.role ?? ""
            };
        }

        // ===================== LOGIN =====================
        public UserAccount? Login(string identifier, string password)
        {
            // identifier bisa email atau username

            string encoded = Uri.EscapeDataString(identifier);

            // filter OR: email eq identifier OR username eq identifier
            // select password_hash juga untuk validasi
            var url =
                $"users?select=id,username,email,role,password_hash&or=(email.eq.{encoded},username.eq.{encoded})&limit=1";

            HttpResponseMessage response;
            try
            {
                response = _http.GetAsync(url).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HTTP error login: {ex.Message}");
                return null;
            }

            var respBody = response.Content.ReadAsStringAsync().Result;

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Supabase error login: {response.StatusCode} - {respBody}");
                return null;
            }

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var users = JsonSerializer.Deserialize<List<SupabaseUserDto>>(respBody, options);

            var u = users != null && users.Count > 0 ? users[0] : null;
            if (u == null) return null;

            // cek password
            if (!VerifyPassword(password, u.password_hash))
                return null;

            return new UserAccount
            {
                Id = u.id,
                Username = u.username ?? "",
                Email = u.email ?? "",
                Role = u.role ?? ""
            };
        }

        // ===================== PASSWORD HASHING =====================

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes); // contoh: "AB12CD..."
        }

        private static bool VerifyPassword(string password, string? storedHash)
        {
            if (string.IsNullOrEmpty(storedHash)) return false;
            var hash = HashPassword(password);
            return string.Equals(hash, storedHash, StringComparison.OrdinalIgnoreCase);
        }

        // DTO untuk baca JSON dari Supabase
        private class SupabaseUserDto
        {
            public long id { get; set; }
            public string? username { get; set; }
            public string? email { get; set; }
            public string? password_hash { get; set; }
            public string? role { get; set; }
        }
    }
}
