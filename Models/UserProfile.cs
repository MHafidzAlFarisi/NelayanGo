namespace NelayanGo.Models
{
    public class UserProfile
    {
        public long UserId { get; set; }

        public string Nama { get; set; } = string.Empty;
        public string Wilayah { get; set; } = string.Empty;
        public string TahunBergabung { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string NIK { get; set; } = string.Empty;
        public string TanggalLahir { get; set; } = string.Empty;
        public string TempatLahir { get; set; } = string.Empty;
        public string NomorTelepon { get; set; } = string.Empty;
        public string Agama { get; set; } = string.Empty;
        public string GolonganDarah { get; set; } = string.Empty;
        public string AlamatKTP { get; set; } = string.Empty;
    }
}
