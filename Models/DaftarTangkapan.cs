using System;
using System.ComponentModel;

namespace NelayanGo.Models
{
    // Model untuk setiap baris data tangkapan di DataGrid
    public class CatchModel
    {
        public string ID { get; set; } = string.Empty;
        public string JamTangkap { get; set; } = string.Empty;
        public string TanggalTangkap { get; set; } = string.Empty;
        public string NamaIkan { get; set; } = string.Empty;
        public double BeratKg { get; set; }
        public string Lokasi { get; set; } = string.Empty;
        public string JamInput { get; set; } = string.Empty;
    }
}