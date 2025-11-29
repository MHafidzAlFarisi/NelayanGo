using System;

namespace NelayanGo.Models
{
    public class HargaIkanModel
    {
        public long KodeIkan { get; set; }
        public string NamaIkan { get; set; } = "";
        public decimal HargaIkan { get; set; }
        public string Wilayah { get; set; } = "";
        public DateTime TanggalUpdate { get; set; }
        public long ID_Admin { get; set; }
    }
}
