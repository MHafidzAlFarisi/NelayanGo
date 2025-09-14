using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hm3
{
    internal class Ikan_tangkapan
    {
        public string NamaIkan { get; set; }
        public string KodeTangkapan { get; set; }
        public string JenisIkan { get; set; }
        public double BeratKg { get; set; }
        public int JumlahIkan { get; set; }
        public Ikan_tangkapan(string namaIkan, string kodeTangkapan, string jenisIkan, double beratKg, int jumlahIkan)
        {
            NamaIkan = namaIkan;
            KodeTangkapan = kodeTangkapan;
            JenisIkan = jenisIkan;
            BeratKg = beratKg;
            JumlahIkan = jumlahIkan;
        }
        public double HitungTotalBerat()
        {
            return BeratKg * JumlahIkan;
        }

    }
}
