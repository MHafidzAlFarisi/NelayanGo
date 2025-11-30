using System;
using System.Windows.Input;

namespace NelayanGo.ViewModels
{
    // Command generik yang bisa dipakai di banyak ViewModel
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?>? _canExecute;

        public RelayCommand(Action<object?> execute,
                            Predicate<object?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Dipakai WPF untuk enable/disable tombol
        public bool CanExecute(object? parameter)
            => _canExecute?.Invoke(parameter) ?? true;

        // Dipanggil ketika Command dijalankan (misalnya tombol ditekan)
        public void Execute(object? parameter)
            => _execute(parameter);

        // WPF pakai event ini untuk update CanExecute otomatis
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        // Bisa dipanggil manual kalau mau paksa re-check CanExecute
        public void RaiseCanExecuteChanged()
            => CommandManager.InvalidateRequerySuggested();
    }
}
