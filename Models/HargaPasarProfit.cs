using System;

namespace NelayanGo.Models
{
    public class HargaPasarProfitItem
    {
        public string NamaIkan { get; set; } = string.Empty;

        // berat tangkapan (kg) untuk row ini
        public double BeratTangkapan { get; set; }

        // total harga untuk row ini (bukan harga per kilo)
        public decimal Harga { get; set; }
    }
}
