﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GasNetwork.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set { _isVisible = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}