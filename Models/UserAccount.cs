using System;

namespace NelayanGo.Models
{
    public class UserAccount
    {
        public int Id { get; set; }
        public string Nama { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // "Admin" atau "Nelayan"
    }
}
