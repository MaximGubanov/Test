using Avalonia;
using System;


namespace GasNetwork.Models
{
    // Это тоже не модель, это чать отображения
    internal class MenuDataContext
    {
        private static void Exit() => Environment.Exit(0);
        private static async void CopyId(string value) => await Application.Current.Clipboard.SetTextAsync(value);
    }
}
