using System;

namespace NelayanGo.Models
{
    // Model untuk daftar harga per kilo di pasar
    public class HargaIkanModel
    {
        public string NamaIkan { get; set; } = string.Empty;
        public decimal HargaPerKilo { get; set; }
    }
}