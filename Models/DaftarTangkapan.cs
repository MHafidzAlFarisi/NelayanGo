using System;
using System.ComponentModel;

namespace NelayanGo.Models
{
    public class IkanTangkapanModel
    {
        public long kodetangkapan { get; set; }
        public DateTime TanggalInput { get; set; }
        public string NamaIkan { get; set; } = string.Empty;
        public string Lokasi { get; set; } = string.Empty;
        public DateTime JamTangkap { get; set; }
        public int BeratKg { get; set; }
        public long TotalHargaIkan { get; set; }
        public long KodeIkan { get; set; }
        public long ID_User { get; set; }
        public long kode_ikan { get; set; }
    }

}