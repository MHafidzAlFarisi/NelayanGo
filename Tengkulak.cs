using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace hm3
{
    internal class Tengkulak
    {
        public  string Username { get; private set; }
        private Dictionary<string, double> daftarHargaPerKg = new Dictionary<string, double>();

        public Tengkulak(Login login)
        {
            if (login.UserRole != Role.tengkulak)
            {
                throw new UnauthorizedAccessException("User is not a tengkulak");
            }
            Username = login.Username;
        }
        public void InputHargaIkan(Ikan_tangkapan ikan)
        {
            Console.Write($"Masukkan harga per kg untuk {ikan.NamaIkan} ({ikan.KodeTangkapan})");
            string? input = Console.ReadLine();
            if (double.TryParse(input, out double hargaPerKg))
            {
                daftarHargaPerKg[ikan.KodeTangkapan] = hargaPerKg;
                Console.WriteLine($"{Username} menetapkan harga {ikan.NamaIkan} = Rp{hargaPerKg}/kg");
            }
            else
            {
                Console.WriteLine("Input tidak valid. Harap masukkan angka.");
            }
        }
    }
}

