using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NelayanGo.DataServices;
using NelayanGo.Models;
using System.ComponentModel;

namespace NelayanGo.ViewModels
{
    public class AdminProfileViewModel : INotifyPropertyChanged
    {
        private readonly DataServiceNelayan _service = new();
        private Nelayan? _currentAdmin;

        public Nelayan? CurrentAdmin
        {
            get => _currentAdmin;
            set { _currentAdmin = value; OnPropertyChanged(nameof(CurrentAdmin)); }
        }

        public AdminProfileViewModel()
        {
            CurrentAdmin = _service.GetDataNelayan("ADM-00");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string prop) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}

