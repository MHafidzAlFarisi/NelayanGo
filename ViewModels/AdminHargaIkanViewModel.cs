using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NelayanGo.DataServices;
using NelayanGo.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace NelayanGo.ViewModels
{
    public class AdminHargaIkanViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<HargalkanModel> DaftarHarga { get; set; }
        public HargalkanModel SelectedHarga { get; set; }

        public AdminHargaIkanViewModel()
        {
            DaftarHarga = new ObservableCollection<HargalkanModel>()
            {
                new HargalkanModel()
                {
                    KodeIkan = 0001,
                    NamaIkan = "Tongkol",
                    HargaIkan = 15000,
                    Wilayah = "Bantul",
                    TanggalUpdate = DateTime.Now
                }
            };
        }

        public void AddHargaBaru(string nama, decimal harga)
        {
            DaftarHarga.Add(new HargalkanModel()
            {
                KodeIkan = 0001,
                NamaIkan = nama,
                HargaIkan = harga,
                TanggalUpdate = DateTime.Now,
                Wilayah = "Bantul"
            });
        }

        public void UpdateHarga(int id, string nama, decimal harga)
        {
            var item = DaftarHarga.FirstOrDefault(x => x.KodeIkan == id);
            if (item != null)
            {
                item.NamaIkan = nama;
                item.HargaIkan = harga;
                item.TanggalUpdate = DateTime.Now;
            }
        }

        public void DeleteHarga(int id)
        {
            var item = DaftarHarga.FirstOrDefault(x => x.KodeIkan == id);
            if (item != null)
                DaftarHarga.Remove(item);
        }
    }
}
