using System;

namespace NelayanGo.Models
{
    // Kelas Model yang menyimpan data
    public class Nelayan
    {
        // Data Pribadi (inisialisasi untuk mengatasi warning nullability)
        public string Nama { get; set; } = string.Empty;
        public string KodeIdentik { get; set; } = string.Empty;
        public string Wilayah { get; set; } = string.Empty;
        public string TahunBergabung { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string NIK { get; set; } = string.Empty;
        public string TanggalLahir { get; set; } = string.Empty;
        public string TempatLahir { get; set; } = string.Empty;
        public string NomorTelepon { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Agama { get; set; } = string.Empty;
        public string GolonganDarah { get; set; } = string.Empty;
        public string AlamatKTP { get; set; } = string.Empty;

        // Data Info
        public int TotalWaktuAktifHari { get; set; }
        public int TotalJarakBerlayarKM { get; set; }
        public int TotalBeratTangkapanTON { get; set; }
        public int TotalJumlahBerlayar { get; set; }
        public int TotalJamBerlayarJAM { get; set; }
        public int TotalJenisTangkapan { get; set; }
    }
}