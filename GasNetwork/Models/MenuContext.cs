using Avalonia;
using System;


namespace GasNetwork.Models
{
    internal class MenuDataContext
    {
        private static void Exit() => Environment.Exit(0);
        private static async void CopyId(string value) => await Application.Current.Clipboard.SetTextAsync(value);
    }
}
