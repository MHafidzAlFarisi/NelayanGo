using System.Text.Json.Serialization;

namespace NelayanGo.Models
{
    public class NelayanModel
    {
        [JsonIgnore]
        public long Id { get; set; }

        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        [JsonPropertyName("nama")]
        public string? Nama { get; set; }

        [JsonIgnore]
        public string? KodeIdentik { get; set; }

        [JsonPropertyName("wilayah")]
        public string? Wilayah { get; set; }

        [JsonPropertyName("tahun_bergabung")]
        public string? TahunBergabung { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("nik")]
        public string? NIK { get; set; }

        [JsonPropertyName("tanggal_lahir")]
        public string? TanggalLahir { get; set; }

        [JsonPropertyName("tempat_lahir")]
        public string? TempatLahir { get; set; }

        [JsonPropertyName("alamat_ktp")]
        public string? AlamatKTP { get; set; }

        [JsonPropertyName("nomor_telepon")]
        public string? NomorTelepon { get; set; }

        [JsonPropertyName("agama")]
        public string? Agama { get; set; }

        [JsonPropertyName("golongan_darah")]
        public string? GolonganDarah { get; set; }

        [JsonIgnore]
        public string? Email { get; set; }
    }
}