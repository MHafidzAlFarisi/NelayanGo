using System;

namespace NelayanGo.Models
{
    // Model untuk profit tangkapan hari ini
    public class ProfitModel
    {
        public string NamaIkan { get; set; } = string.Empty;
        public decimal BeratTangkapan { get; set; } // Diasumsikan dalam KG
        public decimal Harga { get; set; } // Total harga (Berat * HargaPerKilo)
    }
}