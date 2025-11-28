using System;

namespace NelayanGo.Models
{
    public class HargalkanModel
    {
        public string Id { get; set; }
        public string NamaIkan { get; set; }
        public decimal HargaPerKilo { get; set; }
        public string Wilayah { get; set; }
        public DateTime TanggalUpdate { get; set; }
    }
}
