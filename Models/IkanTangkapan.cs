using Postgrest.Attributes;
using Postgrest.Models;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;

namespace NelayanGo.Models
{
    [Table("ikan_tangkapan")]
    public class IkanTangkapan : BaseModel
    {
        [PrimaryKey("id", false)] // false karena auto-increment
        public long Id { get; set; }

        [Column("user_id")]
        public string UserId { get; set; }

        [Column("nama_ikan")]
        public string NamaIkan { get; set; }

        [Column("berat_ikan")]
        public double BeratIkan { get; set; }

        [Column("jam_tangkap")]
        public string JamTangkap { get; set; }

        [Column("tanggal_tangkap")]
        public DateTime TanggalTangkap { get; set; }

        [Column("lokasi")]
        public string Lokasi { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}