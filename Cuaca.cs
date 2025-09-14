using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hm3
{
    public enum KondisiCuaca
    {
        Cerah, Berawan, HujanRingan, HujanLebat, Badai, Kabut
    }

    internal class Cuaca
    {
        public string KodeCuaca { get; set; }
        public DateTime Waktu { get; set; }
        public DateTime Tanggal { get; set; }
        public double suhu { get; set; }
        public KondisiCuaca Kondisi { get; set; }
        public Cuaca(string kodeCuaca, DateTime waktu, DateTime tanggal, double suhu, KondisiCuaca kondisi)
        {
            KodeCuaca = kodeCuaca;
            Waktu = waktu;
            Tanggal = tanggal;
            this.suhu = suhu;
            Kondisi = kondisi;
        }
        public override string ToString()
        {
            return $"{Tanggal:dd/MM/yyyy} {Waktu:HH:mm} | {Kondisi} | {suhu}°C (Kode:{KodeCuaca}";
        }
    }
}

