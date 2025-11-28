using System;
using System.Collections.Generic;

namespace NelayanGo.Models
{
    // Model untuk data TANGKAPAN HARIAN (Grafik Garis)
    public class TangkapanHarianPoint
    {
        public string Jam { get; set; } = string.Empty;
        public double BeratKg { get; set; }
    }

    // Model untuk data TANGKAPAN TAHUNAN (Grafik Batang)
    public class TangkapanTahunanBar
    {
        public string Bulan { get; set; } = string.Empty;
        public double BeratTon { get; set; }
    }

    // Model untuk data JENIS TANGKAPAN (Grafik Batang Kecil)
    public class JenisTangkapanBar
    {
        public string NamaIkan { get; set; } = string.Empty;
        public double TotalKg { get; set; }
    }
}