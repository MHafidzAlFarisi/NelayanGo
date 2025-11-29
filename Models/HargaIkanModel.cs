using System;

namespace NelayanGo.Models
{
    public class HargalkanModel
    {
        public int KodeIkan { get; set; }
        public string NamaIkan { get; set; } = "";
        public decimal HargaIkan { get; set; }
        public string Wilayah { get; set; } = "";
        public DateTime TanggalUpdate { get; set; }
        public string ID_Admin { get; set; }
    }
}
