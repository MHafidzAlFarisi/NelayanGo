using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hm3
{
    public enum Role
    {
        nelayan, tengkulak
    }
    internal class Login
    {
        public string Username {get; private set; }
        public string Password {get; private set; }
        public Role UserRole { get; set; }
        public Login(string username, string password, Role role)
        {
            this.Username = username;
            this.Password = password;
            this.UserRole = role;
        }
        public void UpdatePassword(string newPassword)
        {
            Password = newPassword;
        }
    }
}
